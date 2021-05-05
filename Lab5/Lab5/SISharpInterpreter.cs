using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Interpreter.Collections;

namespace Lab5
{
    internal class SISharpInterpreter
    {
        private readonly Hashtable<string, double> _variables = new();

        public double Execute(params string[] lines)
        {
            SISharpParser parser = new();

            foreach (string line in lines)
            {
                string normalizedLine = Regex.Replace(line, @"\s+", "");

                if(normalizedLine == string.Empty) continue;

                parser.ParseLine(normalizedLine);
            }

            return Traverse(parser.Head);
        }

        public double Traverse(SyntaxTreeNode node)
        {
            switch (node.NodeType)
            {
                case NodeType.StatementList:
                {
                    SyntaxTreeNode[] children = node.ToArray();

                    if (children.Length == 0)
                        return 0;

                    for (int i = 0; i < children.Length - 1; i++)
                    {
                        Traverse(children[i]);
                    }

                    return Traverse(children[^1]);
                }
                case NodeType.Variable:
                {
                    string variableName = node.Data.Variable;

                    if (!node.Any()) return _variables[variableName];

                    try
                    {
                        _variables[variableName] = Traverse(node.GetChild(0));
                    }
                    catch (KeyNotFoundException)
                    {
                        _variables.Add(variableName, Traverse(node.GetChild(0)));
                    }

                    return _variables[variableName];
                }
                case NodeType.Value:
                    return node.Data.Value ?? throw new InvalidOperationException();
                case NodeType.Operator:
                {
                    Operator op = node.Data.Operator ?? throw new InvalidOperationException();

                    SyntaxTreeNode[] children = node.ToArray();
                    SyntaxTreeNode firstOperand = children[0];
                    SyntaxTreeNode secondOperand = children[1];

                    return op switch
                    {
                        Operator.Plus => Traverse(firstOperand) + Traverse(secondOperand),
                        Operator.Minus => Traverse(firstOperand) - Traverse(secondOperand),
                        Operator.Multiply => Traverse(firstOperand) * Traverse(secondOperand),
                        Operator.Divide => Traverse(firstOperand) / Traverse(secondOperand),
                        Operator.Pow => Math.Pow(Traverse(firstOperand), Traverse(secondOperand)),
                        _ => throw new InvalidOperationException()
                    };
                }
                case NodeType.IfStatement:
                {
                    double conditionResult = Traverse(node.GetChild(0));

                    if (conditionResult != 0)
                    {
                        return Traverse(node.GetChild(1));
                    }

                    return Traverse(node.GetChild(2));
                }
                case NodeType.IfCondition or NodeType.IfConditionTrue or NodeType.IfConditionFalse:
                    return Traverse(node.GetChild(0));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

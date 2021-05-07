using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Interpreter.Collections;
using Microsoft.VisualBasic.CompilerServices;

namespace Lab5
{
    internal class SISharpInterpreter
    {
        private readonly Hashtable<string, double> _variables = new();
        private readonly SISharpParser _parser = new();

        public static readonly VariableStorage Storage = new();
        
        public double Execute(string code)
        {
            string[] lines = code.Split(';');
            foreach (string line in lines)
            {
                string normalizedLine = Regex.Replace(line, @"\s+", "");

                if(normalizedLine == string.Empty) continue;

                _parser.ParseLine(normalizedLine);
            }

            //SyntaxTreePrinter.Print(_parser.Head);

            return _parser.Head.Traverse();
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
                        Operator.MoreThan => Traverse(firstOperand) > Traverse(secondOperand) ? 1 : 0,
                        Operator.LessThan => Traverse(firstOperand) < Traverse(secondOperand) ? 1 : 0,
                        Operator.DivideWithRemainder => Traverse(firstOperand) % Traverse(secondOperand),
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

                    if (node.Count() > 2)
                    {
                        return Traverse(node.GetChild(2));
                    }

                    return 0;
                }
                case NodeType.WhileStatement:
                {
                    double whileResult = 0;

                    while (Traverse(node.GetChild(0)) != 0)
                    {
                        whileResult = Traverse(node.GetChild(1));
                    }

                    return whileResult;
                }
                case NodeType.Condition:
                    return Traverse(node.GetChild(0));
                case NodeType.ConditionTrue or NodeType.ConditionFalse:
                    for (int i = 0; i < node.Count() - 1; i++)
                    {
                        Traverse(node.GetChild(i));
                    }
                    return Traverse(node.Last());
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

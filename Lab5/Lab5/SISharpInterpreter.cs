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
                parser.ParseLine(normalizedLine);
            }

            return Traverse(parser.Head);
        }

        public double Traverse(SyntaxTreeNode node)
        {
            if (node.NodeType == NodeType.StatementList)
            {
                SyntaxTreeNode[] children = node.ToArray();

                for (int i = 0; i < children.Length - 1; i++)
                {
                    Traverse(children[i]);
                }

                return Traverse(children[^1]);
            }

            if (node.NodeType == NodeType.Variable)
            {
                string variableName = node.Data.Variable;

                if (node.Any())
                {
                    try
                    {
                        _variables[variableName] = Traverse(node.GetChild(0));
                    }
                    catch (KeyNotFoundException)
                    {
                        _variables.Add(variableName, Traverse(node.GetChild(0)));
                    }
                }

                return _variables[variableName];
            }

            if (node.NodeType == NodeType.Value)
            {
                return node.Data.Value ?? throw new InvalidOperationException();
            }

            if (node.NodeType == NodeType.Operator)
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

            if (node.NodeType == NodeType.IfStatement)
            {
                double conditionResult = Traverse(node.FirstOrDefault(child =>
                    child.NodeType == NodeType.IfCondition));

                if (conditionResult != 0)
                {
                    return Traverse(node.FirstOrDefault(child =>
                        child.NodeType == NodeType.If));
                }

                return Traverse(node.FirstOrDefault(child =>
                    child.NodeType == NodeType.Else));
            }

            if (node.NodeType is NodeType.IfCondition or NodeType.If or NodeType.Else)
            {
                return Traverse(node.GetChild(0));
            }

            throw new NotImplementedException();
        }
    }
}

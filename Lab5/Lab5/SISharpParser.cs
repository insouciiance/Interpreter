using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class SISharpParser
    {
        public readonly SyntaxTreeNode Head = new(NodeType.StatementList, null);

        private static readonly Dictionary<string, int> Operators = new()
        {
            ["("] = 1,
            [")"] = 1,
            ["**"] = 2,
            ["*"] = 3,
            ["/"] = 3,
            ["+"] = 4,
            ["-"] = 4,
        };

        public void ParseLine(string line)
        {
            Head.AddChild(BuildLineNode(line));
        }

        private SyntaxTreeNode BuildLineNode(string line)
        {
            LineType lineType = DiscernLineType(line);
            return lineType switch
            {
                LineType.Statement => BuildStatementNode(line),
                LineType.Assignment => BuildAssignmentNode(line),
                LineType.Expression => BuildExpressionNode(line)
            };
        }

        private SyntaxTreeNode BuildStatementNode(string line)
        {
            return null;
        }

        private SyntaxTreeNode BuildAssignmentNode(string line)
        {
            return null;
        }

        private SyntaxTreeNode BuildExpressionNode(string line)
        {
            string[] postfixExpression = ExpressionConverter.ConvertToPostfix(line);
            Stack<SyntaxTreeNode> nodes = new();

            foreach (string lexeme in postfixExpression)
            {
                SyntaxTreeNode newNode;

                switch (lexeme)
                {
                    case string s when double.TryParse(s, out double d):
                        newNode = new SyntaxTreeNode(NodeType.Value, new(d));
                        nodes.Push(newNode);
                        break;
                    case string op when Operators.ContainsKey(op):
                        Operator operation = op switch
                        {
                            "+" => Operator.Plus,
                            "-" => Operator.Minus,
                            "*" => Operator.Multiply,
                            "/" => Operator.Divide,
                            "**" => Operator.Pow
                        };
                        SyntaxTreeNode rightNode = nodes.Pop();
                        SyntaxTreeNode leftNode = nodes.Pop();
                        newNode = new SyntaxTreeNode(
                            NodeType.Operator, 
                            new(operation),
                            leftNode, 
                            rightNode);
                        nodes.Push(newNode);
                        break;
                }
            }

            return nodes.Pop();
        }


        private LineType DiscernLineType(string line)
        {
            if (line.Split().Contains("="))
            {
                return LineType.Assignment;
            }

            if (line.Contains("if"))
            {
                return LineType.Statement;
            }

            return LineType.Expression;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.SyntaxNodes;

namespace Lab5
{
    internal class ExpressionNodeBuilder : INodeBuilder,INodeBuilder2
    {
        private static readonly Dictionary<string, int> Operators = new()
        {
            ["("] = 0,
            [")"] = 0,
            [">"] = 1,
            ["<"] = 1,
            ["**"] = 2,
            ["*"] = 3,
            ["/"] = 3,
            ["%"] = 3,
            ["+"] = 4,
            ["-"] = 4,
        };

        private readonly string _line;

        public ExpressionNodeBuilder(string line) => _line = line;

        public SyntaxTreeNode Build()
        {
            string[] postfixExpression = ExpressionConverter.ConvertToPostfix(_line);
            Stack<SyntaxTreeNode> nodes = new();

            foreach (string lexeme in postfixExpression)
            {
                SyntaxTreeNode newNode;

                switch (lexeme)
                {
                    case string s when double.TryParse(s, out double d):
                        newNode = new(NodeType.Value, new(d));
                        nodes.Push(newNode);
                        break;
                    case string op when Operators.ContainsKey(op):
                        Operator operation = op switch
                        {
                            "+" => Operator.Plus,
                            "-" => Operator.Minus,
                            "*" => Operator.Multiply,
                            "/" => Operator.Divide,
                            "**" => Operator.Pow,
                            ">" => Operator.MoreThan,
                            "<" => Operator.LessThan,
                            "%" => Operator.DivideWithRemainder,
                            _ => throw new InvalidOperationException()
                        };
                        SyntaxTreeNode rightNode = nodes.Pop();
                        SyntaxTreeNode leftNode = nodes.Pop();
                        newNode = new(
                            NodeType.Operator,
                            new(operation),
                            leftNode,
                            rightNode);
                        nodes.Push(newNode);
                        break;
                    case string variable:
                        newNode = new(NodeType.Variable, new(variable));
                        nodes.Push(newNode);
                        break;
                }
            }

            return nodes.Pop();
        }

        ITraversable INodeBuilder2.Build()
        {
            string[] postfixExpression = ExpressionConverter.ConvertToPostfix(_line);
            Stack<ITraversable> nodes = new();

            foreach (string lexeme in postfixExpression)
            {
                ITraversable newNode;

                switch (lexeme)
                {
                    case string s when double.TryParse(s, out double d):
                        newNode = new ConstantValueNode(d);
                        nodes.Push(newNode);
                        break;
                    case string op when Operators.ContainsKey(op):
                        OperateNodes operation = op switch
                        {
                            "+" => DefaultsOperations.Add,
                            "-" => DefaultsOperations.Subtract,
                            "*" => DefaultsOperations.Multiply,
                            "/" => DefaultsOperations.Divide,
                            "**" => DefaultsOperations.Pow,
                            ">" => DefaultsOperations.MoreThan,
                            "<" => DefaultsOperations.LessThan,
                            "%" => DefaultsOperations.DivideWithRemainder,
                            _ => throw new InvalidOperationException()
                        };
                        ITraversable rightNode = nodes.Pop();
                        ITraversable leftNode = nodes.Pop();
                        newNode = new OperatorNode(rightNode, leftNode, operation);
                        nodes.Push(newNode);
                        break;
                    case string variable:
                        newNode = new VariableNode(variable);
                        nodes.Push(newNode);
                        break;
                }
            }

            return nodes.Pop();
        }
    }
}

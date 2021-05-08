using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.Core;
using Lab5.Core.SyntaxNodes;

namespace Lab5
{
    internal class ExpressionNodeBuilder : INodeBuilder
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

        public ITraversable Build()
        {
            string[] postfixExpression = ExpressionConverter.ConvertToPostfix(_line);
            Stack<ITraversable> nodes = new();

            foreach (string lexeme in postfixExpression)
            {
                ITraversable newNode;

                switch (lexeme)
                {
                    case string s when double.TryParse(s.Replace(',', '.'), 
                        NumberStyles.Any, 
                        CultureInfo.InvariantCulture, 
                        out double d):
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

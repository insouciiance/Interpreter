using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class SISharpParser
    {
        private readonly Dictionary<string, int> _operators = new()
        {
            ["("] = 1,
            [")"] = 1,
            ["**"] = 2,
            ["*"] = 3,
            ["/"] = 3,
            ["+"] = 4,
            ["-"] = 4,
        };

        public TreeNode BuildAbstractSyntaxTree(string expression)
        {
            string[] splitExpression = SplitExpression(expression);
            string[] postfixExpression = ConvertToPostfix(splitExpression);

            Stack<TreeNode> nodes = new();

            foreach (string lexeme in postfixExpression)
            {
                TreeNode newNode;

                switch (lexeme)
                {
                    case string op when _operators.ContainsKey(op):
                        Operator operation = op switch
                        {
                            "+" => Operator.Plus,
                            "-" => Operator.Minus,
                            "*" => Operator.Multiply,
                            "/" => Operator.Divide,
                        };
                        TreeNode rightNode = nodes.Pop();
                        TreeNode leftNode = nodes.Pop();
                        newNode = new TreeNode(null, operation, leftNode, rightNode);
                        nodes.Push(newNode);
                        break;
                    case string s when double.TryParse(s, out double d):
                        newNode = new TreeNode(d, null, null, null);
                        nodes.Push(newNode);
                        break;
                }
            }

            return nodes.Pop();
        }

        private string[] ConvertToPostfix(string[] expression)
        {
            List<string> postfixExpression = new List<string>();
            Stack<string> currentOperators = new Stack<string>();

            foreach (string lexeme in expression)
            {
                if (!_operators.ContainsKey(lexeme))
                {
                    postfixExpression.Add(lexeme);
                }
                else
                {
                    while (currentOperators.Any() && _operators[currentOperators.Peek()] <= _operators[lexeme])
                    {
                        postfixExpression.Add(currentOperators.Pop());
                    }

                    currentOperators.Push(lexeme);
                }
            }

            while (currentOperators.Any())
            {
                postfixExpression.Add(currentOperators.Pop());
            }

            return postfixExpression.ToArray();
        }

        private string[] SplitExpression(string expression)
        {
            string[] operands = expression.Split(_operators.Keys.ToArray(), StringSplitOptions.TrimEntries);
            Queue<string> expressionOperators = new (expression.Split(operands, StringSplitOptions.TrimEntries));

            List<string> splitExpression = new();

            foreach (string operand in operands)
            {
                splitExpression.Add(operand);

                if (expressionOperators.Count > 0)
                {
                    splitExpression.Add(expressionOperators.Dequeue().ToString());
                }
            }

            return splitExpression.ToArray();
        }
    }
}

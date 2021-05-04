using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal static class ExpressionConverter
    {
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

        public static string[] ConvertToPostfix(string expression)
        {
            string[] splitExpression = SplitExpression(expression);
            List<string> postfixExpression = new ();
            Stack<string> currentOperators = new ();

            foreach (string lexeme in splitExpression)
            {
                if (!Operators.ContainsKey(lexeme))
                {
                    postfixExpression.Add(lexeme);
                }
                else
                {
                    while (currentOperators.Any() && Operators[currentOperators.Peek()] <= Operators[lexeme])
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

        private static string[] SplitExpression(string expression)
        {
            string[] operands = expression.Split(Operators.Keys.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            Queue<string> expressionOperators = new(expression.Split(operands, StringSplitOptions.RemoveEmptyEntries));

            List<string> splitExpression = new();

            foreach (string operand in operands)
            {
                splitExpression.Add(operand);

                if (expressionOperators.Count > 0)
                {
                    splitExpression.Add(expressionOperators.Dequeue());
                }
            }

            return splitExpression.ToArray();
        }
    }
}

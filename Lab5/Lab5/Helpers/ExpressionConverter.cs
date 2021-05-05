using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab5
{
    internal static class ExpressionConverter
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
            ["+"] = 4,
            ["-"] = 4,
        };

        public static string[] ConvertToPostfix(string expression)
        {
            string[] splitExpression = SplitExpression(expression);
            List<string> postfixExpression = new();
            Stack<string> currentOperators = new();

            foreach (string lexeme in splitExpression)
            {
                if (!Operators.ContainsKey(lexeme))
                {
                    postfixExpression.Add(lexeme);
                    continue;
                }

                if (lexeme == ")")
                {
                    while (currentOperators.Peek() != "(")
                    {
                        postfixExpression.Add(currentOperators.Pop());
                    }

                    currentOperators.Pop();

                    continue;
                }

                while (currentOperators.Any() &&
                       currentOperators.Peek() != "(" &&
                       Operators[currentOperators.Peek()] <= Operators[lexeme])
                {
                    postfixExpression.Add(currentOperators.Pop());
                }

                currentOperators.Push(lexeme);
            }

            while (currentOperators.Any())
            {
                postfixExpression.Add(currentOperators.Pop());
            }

            return postfixExpression.ToArray();
        }

        private static string[] SplitExpression(string expression)
        {
            List<string> splitExpression = new();

            MatchCollection negativeVariables = Regex.Matches(
                expression, 
                @"(?<![a-zA-Z_0-9]+)-[a-zA-Z_0-9]+");

            foreach (Match match in negativeVariables)
            {
                string matchString = match.Value;
                expression = expression.Replace(matchString, $"0{matchString}");
            }

            foreach (Match match in Regex.Matches(
                expression,
                @"([*][*])|([><*+/)(])|((?<=[a-zA-Z_0-9]+)-)|(-?\d+(,\d+)?)|([a-zA-Z_0-9]+)"))
            {
                splitExpression.Add(match.Value);
            }

            return splitExpression.ToArray();
        }
    }
}

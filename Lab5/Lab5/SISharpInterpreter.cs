using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Interpreter.Collections;
using Microsoft.VisualBasic;

namespace Lab5
{
    internal class SISharpInterpreter
    {
        private readonly Hashtable<string, double> _variables = new ();

        public void Execute(params string[] lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(BuildAbstractSyntaxTree(line).Evaluate());
            }
        }

        private TreeNode BuildAbstractSyntaxTree(string expression)
        {
            string[] operators = { "+", "-", "*", "/", "(", ")" };

            string[] splitExpression = SplitExpression(expression);
            ReplaceVariablesWithValues(splitExpression);
            string[] postfixExpression = ConvertToPostfix(splitExpression);

            Stack<TreeNode> nodes = new();

            foreach (string lexeme in postfixExpression)
            {
                TreeNode newNode;
                switch (lexeme)
                {
                    case string op when operators.Contains(op):
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

        public string[] ConvertToPostfix(string[] expression)
        {
            Dictionary<string, int> operationsPriority = new()
            {
                ["("] = 1,
                [")"] = 1,
                ["**"] = 2,
                ["*"] = 3,
                ["/"] = 3,
                ["+"] = 4,
                ["-"] = 4,
            };

            List<string> postfixExpression = new List<string>();
            Stack<string> currentOperators = new Stack<string>();

            foreach (string lexeme in expression)
            {
                if (!operationsPriority.ContainsKey(lexeme))
                {
                    postfixExpression.Add(lexeme);
                }
                else
                {
                    while (currentOperators.Any() && operationsPriority[currentOperators.Peek()] <= operationsPriority[lexeme])
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

        private void ReplaceVariablesWithValues(string[] splitExpression)
        {
            string[] operators = {"+", "-", "*", "/", "(", ")"};

            for (int i = 0; i < splitExpression.Length; i++)
            {
                if (!operators.Contains(splitExpression[i]) && !double.TryParse(splitExpression[i], out _))
                {
                    splitExpression[i] = _variables[splitExpression[i]].ToString();
                } 
            }
        }

        public string[] SplitExpression(string expression)
        {
            char[] operators = {'+', '-', '*', '/', '(', ')'};
            Queue<char> expressionOperators = new (expression.Where(operators.Contains));

            string[] operands = expression.Split(operators);

            List<string> splitExpression = new ();

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

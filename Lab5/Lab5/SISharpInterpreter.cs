using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Interpreter.Collections;

namespace Lab5
{
    internal class SISharpInterpreter
    {
        private readonly Hashtable<string, double> _variables = new ();

        public void Execute(params string[] lines)
        {
            SISharpParser parser = new ();

            foreach (string line in lines)
            {
                parser.ParseLine(line);
            }

            Console.WriteLine(Traverse(parser.Head));
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

            if (node.NodeType == NodeType.Value)
            {
                return node.Data.Value ?? throw new InvalidOperationException();
            }

            if (node.NodeType == NodeType.Variable)
            {
                return _variables[node.Data.Variable];
            }

            if (node.NodeType == NodeType.Operator)
            {
                Operator op = node.Data.Operator ?? throw new InvalidOperationException();

                SyntaxTreeNode[] children = node.ToArray();
                double result = Traverse(children[0]);

                if (op == Operator.Plus)
                {
                    for (int i = 1; i < children.Length; i++)
                    {
                        result += Traverse(children[i]);
                    }

                    return result;
                }

                if (op == Operator.Minus)
                {
                    for (int i = 1; i < children.Length; i++)
                    {
                        result -= Traverse(children[i]);
                    }

                    return result;
                }

                if (op == Operator.Multiply)
                {
                    for (int i = 1; i < children.Length; i++)
                    {
                        result *= Traverse(children[i]);
                    }

                    return result;
                }

                if (op == Operator.Divide)
                {
                    for (int i = 1; i < children.Length; i++)
                    {
                        result /= Traverse(children[i]);
                    }

                    return result;
                }

                if (op == Operator.Pow)
                {
                    for (int i = 1; i < children.Length; i++)
                    {
                        result = Math.Pow(result, Traverse(children[i]));
                    }

                    return result;
                }
            }

            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
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
                Console.WriteLine(parser.BuildAbstractSyntaxTree(line).Evaluate());
            }
        }
    }
}

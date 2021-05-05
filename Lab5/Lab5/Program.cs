using System;
using System.Threading.Tasks;
using Interpreter.Collections;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            SISharpInterpreter interpreter = new ();
            string code = string.Empty;

            string newLine;
            while ((newLine = Console.ReadLine()) != string.Empty)
            {
                code += newLine;
            }

            Console.WriteLine(interpreter.Execute(code));

            Console.ReadKey();
        }
    }
}
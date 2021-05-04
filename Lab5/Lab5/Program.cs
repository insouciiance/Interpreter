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
            while (true)
            {
                Console.WriteLine(interpreter.Execute(Console.ReadLine()));
            }
        }
    }
}
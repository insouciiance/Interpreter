using System;
using Interpreter.Collections;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            string expression = "1+3*3+7-32/4";
            new SISharpInterpreter().Execute(expression);
            Console.ReadKey();
        }
    }
}
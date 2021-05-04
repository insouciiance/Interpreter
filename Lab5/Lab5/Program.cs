using System;
using Interpreter.Collections;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            SISharpInterpreter interpreter = new();
            while (true)
            {
                interpreter.Execute(Console.ReadLine());
            }
        }
    }
}
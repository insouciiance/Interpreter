using System;
using Interpreter.Collections;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            new SISharpInterpreter().Execute(Console.ReadLine());
            Console.ReadKey();
        }
    }
}
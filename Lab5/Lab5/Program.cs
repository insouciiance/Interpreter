using System;
using System.Threading.Tasks;
using Interpreter.Collections;

namespace Lab5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using SISharpReader reader = new(args[0]);
            Console.WriteLine(await reader.ExecuteAsync());
            Console.ReadKey();
        }
    }
}
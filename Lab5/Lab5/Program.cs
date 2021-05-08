using System;
using System.IO;
using System.Threading.Tasks;
using Interpreter.Collections;

namespace Lab5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SISharpInterpreter interpreter = new ();

            string path = Console.ReadLine();

            using StreamReader reader = new(path ?? throw new ArgumentNullException());
            string code = await reader.ReadToEndAsync();

            interpreter.Execute(code);

            Console.ReadKey();
        }
    }
}
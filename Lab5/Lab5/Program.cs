using System;
using System.IO;
using System.Threading.Tasks;
using Interpreter.Collections;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            SISharpInterpreter interpreter = new ();
            
            string path = @"C:\Users\danvu\Documents\GitHubPP\Interpreter\Lab5\Lab5\code.txt";
            using StreamReader reader = new StreamReader(path);
            Console.WriteLine(interpreter.Execute(reader.ReadToEnd()));
        }
        }
    }
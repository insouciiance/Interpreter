using System;
using Interpreter.Collections;
using Lab5.Commands;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeProgramm().Visit(); // Run program
        }

        private static IVisitable MakeProgramm()
        {
            AssignVariable<int> assign = new AssignVariable<int>("var",12);
            DebugPrintMassage m = new DebugPrintMassage(new GetConstantValue<string>("Hello world!"));
            var assign2 = new AssignVariable<int>("var", 13);
            m.Next = assign2;
            DebugPrintMassage m2 = new DebugPrintMassage(new GetVariable<string>("var"));
            assign2.Next = m2;
            return assign;
        }
    }
}
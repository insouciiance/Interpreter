using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal static class SyntaxTreePrinter
    {
        public static void Print(SyntaxTreeNode n)
        {
            int paddingCount = 0;

            Traverse(n);

            void Traverse(SyntaxTreeNode node)
            {
                if (node == null) return;

                string prettyPadding = node.Count() > 1 ? "├──" : "└──";
                Console.Write($"{string.Concat(Enumerable.Repeat("| ", paddingCount))}{prettyPadding} ");

                Console.ForegroundColor = (ConsoleColor)(paddingCount % 10 + 5);
                Console.WriteLine(node);
                Console.ForegroundColor = ConsoleColor.Gray;

                paddingCount++;
                foreach (SyntaxTreeNode child in node)
                {
                    Traverse(child);
                }
                paddingCount--;
            }
        }
    }
}

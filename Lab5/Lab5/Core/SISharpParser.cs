using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.SyntaxNodes;

namespace Lab5
{
    internal class SISharpParser
    {
        public StatementListNode Head { get; }

        public SISharpParser()
        {
            Head = new StatementListNode();
        }

        public void ParseLine(string line)
        {
            SyntaxTreeNodeBuilder builder = new (line);
            Head.AddSubNode(builder.Build());
        }
    }
}

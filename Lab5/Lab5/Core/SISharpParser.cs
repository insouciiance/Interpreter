using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class SISharpParser
    {
        public readonly SyntaxTreeNode Head = new(NodeType.StatementList, null);

        public void ParseLine(string line)
        {
            SyntaxTreeNodeBuilder builder = new (line);
            Head.AddChild(builder.Build());
        }
    }
}

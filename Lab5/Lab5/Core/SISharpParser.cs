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
        public ITraversable Head { get; private set; }

        public void ParseLine(string line)
        {
            SyntaxTreeNodeBuilder builder = new (line);
            Head = builder.Build();
        }
    }
}

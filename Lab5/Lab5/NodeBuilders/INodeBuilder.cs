using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal interface INodeBuilder
    {
        SyntaxTreeNode Build();
    }
    internal interface INodeBuilder2
    {
        ITraversable Build();
    }
}

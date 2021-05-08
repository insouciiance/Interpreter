using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.Core;

namespace Lab5
{
    internal interface INodeBuilder
    {
        ITraversable Build();
    }
}

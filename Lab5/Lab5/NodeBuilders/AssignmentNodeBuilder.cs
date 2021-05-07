using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.SyntaxNodes;

namespace Lab5
{
    internal class AssignmentNodeBuilder : INodeBuilder
    {
        private readonly string _line;

        public AssignmentNodeBuilder(string line) => _line = line;

        public ITraversable Build()
        {
            string[] splitAssignment = _line.Split("=");

            ITraversable setter = new ExpressionNodeBuilder(splitAssignment[1]).Build();
            VariableNode node = new VariableNode(splitAssignment[0],setter);

            return node;
        }
    }
}

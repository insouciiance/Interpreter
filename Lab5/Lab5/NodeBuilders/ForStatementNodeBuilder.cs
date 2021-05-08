using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.NodeBuilders
{
    internal class ForStatementNodeBuilder : INodeBuilder
    {
        private string _line;

        public ForStatementNodeBuilder(string line) => _line = line;

        public ITraversable Build()
        {
            string[] splitFor = _line.Split(new string[]{"for(", "){", "}"}, StringSplitOptions.RemoveEmptyEntries);
            string[] innerConditions = splitFor[0].Split("", StringSplitOptions.RemoveEmptyEntries);

            ITraversable firstInner = new SyntaxTreeNodeBuilder(innerConditions[0]).Build();
            ITraversable secondInner = new SyntaxTreeNodeBuilder(innerConditions[1]).Build();
            ITraversable thirdInner = new SyntaxTreeNodeBuilder(innerConditions[2]).Build();

            ITraversable body = new SyntaxTreeNodeBuilder(splitFor[1]).Build();

            return new ForStatementNode(firstInner, secondInner, thirdInner, body);
        }
    }
}

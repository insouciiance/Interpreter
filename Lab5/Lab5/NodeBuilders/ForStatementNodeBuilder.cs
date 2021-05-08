using System;
using Lab5.Core;
using Lab5.Core.SyntaxNodes;

namespace Lab5.NodeBuilders
{
    internal class ForStatementNodeBuilder : INodeBuilder
    {
        private string _line;

        public ForStatementNodeBuilder(string line) => _line = line;

        public ITraversable Build()
        {
            string[] splitFor = _line.Split(new string[]{"for(", "){", "}"}, StringSplitOptions.RemoveEmptyEntries);
            string[] innerConditions = splitFor[0].Split(".", StringSplitOptions.RemoveEmptyEntries);

            ITraversable initVariable = new SyntaxTreeNodeBuilder(innerConditions[0]).Build();
            ITraversable condition = new SyntaxTreeNodeBuilder(innerConditions[1]).Build();
            ITraversable variableModifier = new SyntaxTreeNodeBuilder(innerConditions[2]).Build();

            ITraversable body = new SyntaxTreeNodeBuilder(splitFor[1]).Build();

            return new ForNode( condition,initVariable, variableModifier, body);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.NodeBuilders
{
    internal class WhileStatementNodeBuilder : INodeBuilder
    {
        private readonly string _line;

        public WhileStatementNodeBuilder(string line) => _line = line;

        public SyntaxTreeNode Build()
        {
            string[] splitLine = _line.Split(
                new []{"while(", "){", "}"}, 
                StringSplitOptions.RemoveEmptyEntries);

            string condition = splitLine[0];
            string trueBlock = splitLine[1];
            
            SyntaxTreeNode statementNode = new (NodeType.WhileStatement, null);

            SyntaxTreeNode conditionNode = new (NodeType.Condition, null);
            conditionNode.AddChild(new SyntaxTreeNodeBuilder(condition).Build());

            SyntaxTreeNode trueNode = new(NodeType.ConditionTrue, null);
            string[] trueBlockLines = trueBlock.Split('|');

            foreach (string line in trueBlockLines)
            {
                trueNode.AddChild(new SyntaxTreeNodeBuilder(line).Build());
            }

            statementNode.AddChild(conditionNode);
            statementNode.AddChild(trueNode);

            return statementNode;
        }
    }
}

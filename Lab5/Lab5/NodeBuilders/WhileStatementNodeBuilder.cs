using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.Core;
using Lab5.Core.SyntaxNodes;

namespace Lab5.NodeBuilders
{
    internal class WhileStatementNodeBuilder : INodeBuilder
    {
        private readonly string _line;

        public WhileStatementNodeBuilder(string line) => _line = line;

        public ITraversable Build()
        {
            string[] splitLine = _line.Split(
                new []{"while", "do"}, 
                StringSplitOptions.RemoveEmptyEntries);

            string condition = splitLine[0];
            string trueBlock = splitLine[1];
            
            ConditionNode conditionNode = new ConditionNode(new SyntaxTreeNodeBuilder(condition).Build());

            StatementListNode statementNode = new StatementListNode();
            string[] trueBlockLines = trueBlock.Split("|");

            foreach (string line in trueBlockLines)
            {
                statementNode.AddSubNode(new SyntaxTreeNodeBuilder(line).Build());
            }
            return new WhileNode(conditionNode,statementNode);
        }
    }
}

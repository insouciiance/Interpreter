using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.Core;
using Lab5.Core.SyntaxNodes;

namespace Lab5
{
    internal class IfStatementNodeBuilder : INodeBuilder
    {
        private readonly string _line;

        public IfStatementNodeBuilder(string line) => _line = line;
        
        public ITraversable Build()
        {
            string[] splitStatement = _line.Split(
                new [] { "if", "then", "else" },
                StringSplitOptions.RemoveEmptyEntries);

            string statementBody = splitStatement[0];
            string trueBlock = splitStatement[1];
            
            ConditionNode conditionNode = new ConditionNode(new SyntaxTreeNodeBuilder(statementBody).Build());
            
            StatementListNode ifBody = new StatementListNode();
            string[] trueBlockLines = trueBlock.Split("->");
            
            foreach (string line in trueBlockLines)
            {
                ifBody.AddSubNode(new SyntaxTreeNodeBuilder(line).Build());
            }

            if (splitStatement.Length <= 2) return new IfStatementNode(conditionNode,ifBody);

            string elseBlock = splitStatement[2];
            string[] elseBlockLines = elseBlock.Split("->");

            StatementListNode elseBody = new StatementListNode();
            
            foreach (string line in elseBlockLines)
            {
                elseBody.AddSubNode(new SyntaxTreeNodeBuilder(line).Build());
            }
            
            return new IfStatementNode(conditionNode,ifBody,elseBody);
        }
    }
}

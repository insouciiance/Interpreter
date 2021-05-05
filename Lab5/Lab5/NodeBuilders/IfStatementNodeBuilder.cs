using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class IfStatementNodeBuilder : INodeBuilder
    {
        private readonly string _line;

        public IfStatementNodeBuilder(string line) => _line = line;

        public SyntaxTreeNode Build()
        {
            string[] splitStatement = _line.Split(
                new string[] { "if(", "){", "}else{", "}" },
                StringSplitOptions.RemoveEmptyEntries);

            string statementBody = splitStatement[0];
            string trueBlock = splitStatement[1];

            SyntaxTreeNode statementNode = new(NodeType.IfStatement, null);

            SyntaxTreeNode conditionNode = new(NodeType.IfCondition, null);
            conditionNode.AddChild(new SyntaxTreeNodeBuilder(statementBody).Build());

            SyntaxTreeNode trueNode = new(NodeType.IfConditionTrue, null);
            trueNode.AddChild(new SyntaxTreeNodeBuilder(trueBlock).Build());

            statementNode.AddChild(conditionNode);
            statementNode.AddChild(trueNode);

            if (splitStatement.Length <= 2) return statementNode;

            string elseBlock = splitStatement[2];
            SyntaxTreeNode falseNode = new(NodeType.IfConditionFalse, null);
            falseNode.AddChild(new SyntaxTreeNodeBuilder(elseBlock).Build());
            statementNode.AddChild(falseNode);

            return statementNode;
        }
    }
}

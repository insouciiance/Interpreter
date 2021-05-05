using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class AssignmentNodeBuilder : INodeBuilder
    {
        private readonly string _line;

        public AssignmentNodeBuilder(string line) => _line = line;

        public SyntaxTreeNode Build()
        {
            string[] splitAssignment = _line.Split("=");

            SyntaxTreeNode expressionTree = new ExpressionNodeBuilder(splitAssignment[^1]).Build();
            SyntaxTreeNode assignmentNode = new(NodeType.Variable, new(splitAssignment[^2]));
            assignmentNode.AddChild(expressionTree);

            for (int i = splitAssignment.Length - 3; i >= 0; i--)
            {
                SyntaxTreeNode newAssignmentNode = new(NodeType.Variable, new(splitAssignment[i])); ;
                newAssignmentNode.AddChild(assignmentNode);
                assignmentNode = newAssignmentNode;
            }

            return assignmentNode;
        }
    }
}

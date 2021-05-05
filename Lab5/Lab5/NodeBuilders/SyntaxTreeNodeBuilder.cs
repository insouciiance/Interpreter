using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class SyntaxTreeNodeBuilder : INodeBuilder
    {
        private readonly string _line;

        public SyntaxTreeNodeBuilder(string line) => _line = line;

        public SyntaxTreeNode Build()
        {
            LineType lineType = DiscernLineType(_line);
            return lineType switch
            {
                LineType.IfStatement => new IfStatementNodeBuilder(_line).Build(),
                LineType.Assignment => new AssignmentNodeBuilder(_line).Build(),
                LineType.Expression => new ExpressionNodeBuilder(_line).Build(),
                _ => throw new InvalidOperationException()
            };
        }

        private static LineType DiscernLineType(string line)
        {
            if (line.Contains("if"))
            {
                return LineType.IfStatement;
            }

            if (line.Contains("="))
            {
                return LineType.Assignment;
            }

            return LineType.Expression;
        }
    }
}

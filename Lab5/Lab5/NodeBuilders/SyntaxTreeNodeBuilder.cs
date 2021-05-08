using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.Core;
using Lab5.NodeBuilders;

namespace Lab5
{
    internal class SyntaxTreeNodeBuilder : INodeBuilder
    {
        private readonly string _line;

        public SyntaxTreeNodeBuilder(string line) => _line = line;

        public ITraversable Build()
        {
            LineType lineType = DiscernLineType(_line);

            INodeBuilder builder = lineType switch
            {
                LineType.IfStatement => new IfStatementNodeBuilder(_line),
                LineType.Assignment => new AssignmentNodeBuilder(_line),
                LineType.Expression => new ExpressionNodeBuilder(_line),
                LineType.WhileStatement => new WhileStatementNodeBuilder(_line),
                _ => throw new InvalidOperationException()
            };

            return builder.Build();
        }

        private static LineType DiscernLineType(string line)
        {
            if (line.Contains("while"))
            {
                return LineType.WhileStatement;
            }

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

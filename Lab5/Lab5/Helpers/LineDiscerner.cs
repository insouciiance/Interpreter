using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Helpers
{
    internal static class LineDiscerner
    {
        public static LineType DiscernLineType(string line)
        {
            if (line.StartsWith("for"))
            {
                return LineType.ForStatement;
            }

            if (line.StartsWith("while"))
            {
                return LineType.WhileStatement;
            }

            if (line.StartsWith("if"))
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal enum NodeType
    {
        Value,
        Variable,
        Operator,
        Condition,
        ConditionTrue,
        ConditionFalse,
        StatementList,
        WhileStatement,
        IfStatement
    }
}

﻿using System;
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
        IfStatement,
        IfCondition,
        IfConditionTrue,
        IfConditionFalse,
        StatementList
    }
}

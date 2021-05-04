using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class NodeData
    {
        public string Variable { get; }
        public double? Value { get; }
        public Operator? Operator { get; }

        public NodeData(){}
        public NodeData(string variable) => Variable = variable;
        public NodeData(double value) => Value = value;
        public NodeData(Operator op) => Operator = op;
    }
}

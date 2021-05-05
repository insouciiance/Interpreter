using System;

namespace Lab5.SyntaxNodes
{
    public class ConstantValueNode : ITraversable
    {
        private double? _value;

        public ConstantValueNode(double? value)
        {
            _value = value;
        }

        public double Traverse()
        {
            return _value ?? throw new InvalidOperationException();;
        }
    }
}
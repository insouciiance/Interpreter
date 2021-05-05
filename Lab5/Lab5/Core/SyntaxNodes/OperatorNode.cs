using System;

namespace Lab5.SyntaxNodes
{
    public delegate double OperateNodes(double x,double y);
    
    public class OperatorNode : ITraversable
    {
        private ITraversable _firstOperand;
        private ITraversable _secondOperand;
        private OperateNodes _operation;

        public OperatorNode(ITraversable firstOperand, ITraversable secondOperand, OperateNodes operation)
        {
            _firstOperand = firstOperand;
            _secondOperand = secondOperand;
            _operation = operation;
        }

        public double Traverse()
        {
            return _operation(_firstOperand.Traverse(), _secondOperand.Traverse());
        }

        public static OperateNodes Add => (x, y) => x + y;
        public static OperateNodes Subtract => (x, y) => x - y;
        public static OperateNodes Multiply => (x, y) => x * y;
        public static OperateNodes Divide => (x, y) => x / y;
        public static OperateNodes Pow => Math.Pow;
        public static OperateNodes DivideWithRemainder => (x, y) => x % y;
        public static OperateNodes MoreThan => (x, y) => x > y ? 1 : 0;
        public static OperateNodes LessThan => (x, y) => x < y ? 1 : 0;

    }
}
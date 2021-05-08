using System;

namespace Lab5.Core.SyntaxNodes
{
    public delegate double OperateNodes(double x, double y);

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

        public void DebugPrint(int paddingCount)
        {
            Console.WriteLine("operaion node");
            _secondOperand.DebugPrint(paddingCount);
            _firstOperand.DebugPrint(paddingCount);
        }
    }
}
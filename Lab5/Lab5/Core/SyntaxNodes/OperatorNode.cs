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
    }
}
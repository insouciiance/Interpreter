using System;

namespace Lab5.Core.SyntaxNodes
{
    public class ForNode : ITraversable
    {
        private ITraversable _initVariable;
        private ITraversable _condition;
        private ITraversable _modifiedVariable;
        private ITraversable _forBody;

        public ForNode(ITraversable initVariable, ITraversable condition, ITraversable modifiedVariable, ITraversable forBody)
        {
            _initVariable = initVariable;
            _condition = condition;
            _modifiedVariable = modifiedVariable;
            _forBody = forBody;
        }

        public ForNode(ITraversable condition, ITraversable modifiedVariable, ITraversable forBody)
        {
            _condition = condition;
            _modifiedVariable = modifiedVariable;
            _forBody = forBody;
        }

        public double Traverse()
        {
            _initVariable?.Traverse();
            while (_condition.Traverse() > 0)
            {
                _forBody.Traverse();
                _modifiedVariable.Traverse();
            }
            return 0;
        }

        public void DebugPrint(int paddingCount)
        {
            Console.WriteLine("for node");
            paddingCount++;
            _condition.DebugPrint(paddingCount);
            _initVariable.DebugPrint(paddingCount);
            _modifiedVariable.DebugPrint(paddingCount);
            _forBody.DebugPrint(paddingCount);
            paddingCount--;
        }
    }
}
namespace Lab5.SyntaxNodes
{
    public class IfStatementNode : ITraversable
    {

        private ITraversable _condition;
        private ITraversable _ifBody;
        private ITraversable _elseBody;

        public IfStatementNode(ITraversable condition, ITraversable ifBody, ITraversable elseBody)
        {
            _condition = condition;
            _ifBody = ifBody;
            _elseBody = elseBody;
        }
        
        public IfStatementNode(ITraversable condition, ITraversable ifBody)
        {
            _condition = condition;
            _ifBody = ifBody;
        }

        public double Traverse()
        {
            double conditionResult = _condition.Traverse();

            return conditionResult == 0 ? _ifBody.Traverse() : _elseBody?.Traverse() ?? 0;
        }
    }
}
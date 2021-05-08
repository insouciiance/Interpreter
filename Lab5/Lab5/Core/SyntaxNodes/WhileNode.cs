namespace Lab5.SyntaxNodes
{
    public class WhileNode : ITraversable
    {

        private ITraversable _condition;
        private ITraversable _body;

        public WhileNode(ITraversable condition, ITraversable body)
        {
            _condition = condition;
            _body = body;
        }

        public double Traverse()
        {
            double whileResult = 0;

            while (_condition.Traverse() == 1)
            {
                whileResult = _body.Traverse();
            }

            return whileResult;
        }
    }
}
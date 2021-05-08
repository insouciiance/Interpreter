namespace Lab5.Core.SyntaxNodes
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

        public void DebugPrint(int paddingCount)
        {
            /*string prettyPadding = "├──" : "└──";
            Console.Write($"{string.Concat(Enumerable.Repeat("| ", paddingCount))}{prettyPadding} ");

            Console.ForegroundColor = (ConsoleColor)(paddingCount % 10 + 5);
            Console.WriteLine(node);
            Console.ForegroundColor = ConsoleColor.Gray;

            paddingCount++;
            foreach (SyntaxTreeNode child in node)
            {
                Traverse(child);
            }
            paddingCount--;*/
            //Console.WriteLine(Enumerable.Repeat("| ", paddingCount) + "├──"  + "while node");
            paddingCount++;
            _condition.DebugPrint(paddingCount);
            _body.DebugPrint(paddingCount);
            paddingCount--;
        }
    }
}
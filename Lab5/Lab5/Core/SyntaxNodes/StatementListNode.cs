using System.Collections.Generic;

namespace Lab5.SyntaxNodes
{
    public class StatementListNode : ITraversable
    {
        
        private readonly List<ITraversable> _children = new();

        public void AddSubNode(ITraversable node)
        {
            _children.Add(node);
        }
        
        public double Traverse()
        {
            if (_children.Count == 0)
                return 0;

            for (int i = 0; i < _children.Count - 1; i++)
            {
                _children[i].Traverse();
            }

            return _children[^1].Traverse();
        }
    }
}
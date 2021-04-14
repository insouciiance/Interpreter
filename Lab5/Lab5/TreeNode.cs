using System.Linq;
using Interpreter.Collections;

namespace Lab5
{
    public delegate void TreeVisitor<T>(T nodeData);

    public class TreeNode<T>
    {
        private readonly T _data;
        private readonly LinkedList<TreeNode<T>> _children;

        public TreeNode(T data)
        {
            _data = data;
            _children = new LinkedList<TreeNode<T>>();
        }

        public void AddChild(T data)
        {
            _children.AddFirst(new TreeNode<T>(data));
        }

        public TreeNode<T> GetChild(int i) =>
            _children.FirstOrDefault(n => --i == 0);

        public void Traverse(TreeNode<T> node, TreeVisitor<T> visitor)
        {
            visitor(node._data);
            foreach (TreeNode<T> kid in node._children)
                Traverse(kid, visitor);
        }
    }
}
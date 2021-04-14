using System.Collections.Generic;

namespace Lab5
{
    public delegate void TreeVisitor<T>(T nodeData);
    public class TreeNode<T>
    {
        private T data;
        private LinkedList<TreeNode<T>> children;

        public TreeNode(T data)
        {
            this.data = data;
            children = new LinkedList<TreeNode<T>>();
        }

        public void AddChild(T data)
        {
            children.AddFirst(new TreeNode<T>(data));
        }

        public TreeNode<T> GetChild(int i)
        {
            foreach (TreeNode<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public void Traverse(TreeNode<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.data);
            foreach (TreeNode<T> kid in node.children)
                Traverse(kid, visitor);
        }
    }
}
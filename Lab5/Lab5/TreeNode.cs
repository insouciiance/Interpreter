using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Interpreter.Collections;

namespace Lab5
{
    internal record TreeNode(double? Value, Operator? Operation, TreeNode LeftNode, TreeNode RightNode)
    {
        public double Evaluate() => Value ?? Operation switch
        {
            Operator.Plus => LeftNode.Evaluate() + RightNode.Evaluate(),
            Operator.Minus => LeftNode.Evaluate() - RightNode.Evaluate(),
            Operator.Multiply => LeftNode.Evaluate() * RightNode.Evaluate(),
            Operator.Divide => LeftNode.Evaluate() / RightNode.Evaluate()
        };
    }
}
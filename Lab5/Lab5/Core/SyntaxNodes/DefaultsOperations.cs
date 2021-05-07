using System;

namespace Lab5.SyntaxNodes
{
    public static class DefaultsOperations
    {
        public static OperateNodes Add => (x, y) => x + y;
        public static OperateNodes Subtract => (x, y) => x - y;
        public static OperateNodes Multiply => (x, y) => x * y;
        public static OperateNodes Divide => (x, y) => x / y;
        public static OperateNodes Pow => Math.Pow;
        public static OperateNodes DivideWithRemainder => (x, y) => x % y;
        public static OperateNodes MoreThan => (x, y) => x > y ? 1 : 0;
        public static OperateNodes LessThan => (x, y) => x < y ? 1 : 0;
    }
}
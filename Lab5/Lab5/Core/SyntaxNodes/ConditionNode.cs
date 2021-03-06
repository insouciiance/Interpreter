using System;
using System.Collections.Generic;

namespace Lab5.Core.SyntaxNodes
{
    public class ConditionNode : ITraversable
    {
        private Condition _condition;
        private List<ITraversable> _subConditions;

        public ConditionNode(List<ITraversable> subConditions, Condition condition)
        {
            _subConditions = subConditions;
            _condition = condition;
        }

        public ConditionNode(ITraversable subCondition)
        {
            _subConditions = new List<ITraversable> {subCondition};
            _condition = Condition.All;
        }

        public double Traverse()
        {
            foreach (var subCondition in _subConditions)
            {
                double result = subCondition.Traverse();
                switch (_condition)
                {
                    case Condition.All when result == 0:
                        return 0;
                    case Condition.Any when result == 1:
                        return 1;
                }
            }

            return _condition == Condition.All ? 1 : 0;
        }

        public void DebugPrint(int paddingCount)
        {
            Console.WriteLine("condition node");
            foreach (var subCondition in _subConditions)
            {
                subCondition.DebugPrint(paddingCount);
            }
        }

        public enum Condition
        {
            All,
            Any
        }
    }
}
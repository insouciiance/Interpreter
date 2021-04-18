using System;

namespace Lab5.Commands
{
    public class DebugPrintMassage : IVisitable,IParent
    {
        public IVisitable Next { get; set; }

        private IVisitable<string> _value;

        public DebugPrintMassage(IVisitable<string> value)
        {
            this._value = value;
        }

        public void Visit()
        {
            Console.WriteLine(_value.Visit());
        }

    }
}
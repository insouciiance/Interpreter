using System.Collections.Generic;

namespace Lab5.SyntaxNodes
{
    public class VariableNode : ITraversable
    {
        private string _name;
        private ITraversable _setter;

        public VariableNode(string name)
        {
            _name = name;
        }

        public VariableNode(string name, ITraversable setter)
        {
            _name = name;
            _setter = setter;
        }

        public double Traverse()
        {
            if(_setter != null)
                SISharpInterpreter.Storage.SetVariable(_name,_setter.Traverse());
            return SISharpInterpreter.Storage.GetVariable(_name);
        }
    }
}
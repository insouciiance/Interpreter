using System.Collections.Generic;
using Interpreter.Collections;

namespace Lab5
{
    public class VariableStorage
    {
        private Dictionary<string, double> _variables = new();

        public void SetVariable(string name, double value)
        {
            if (_variables.ContainsKey(name))
                _variables[name] = value;
            else
            {
                _variables.Add(name, value);
            }
        }

        public double GetVariable(string name)
        {
            return _variables[name];
        }
    }
}
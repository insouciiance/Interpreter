using Interpreter.Collections;

namespace Lab5
{
    public class VariableStorage
    {
        private readonly Hashtable<string, double> _variables = new();

        public void SetVariable(string name,double value)
        { 
            _variables.Add(name,value);
        }
        public double GetVariable(string name)
        {
            return _variables[name];
        }
    }
}
using Interpreter.Collections;

namespace Lab5
{
    public class VariableStorage
    {
        private readonly Hashtable<string, double> _variables = new();

        public Hashtable<string, double> Variables => _variables;
    }
}
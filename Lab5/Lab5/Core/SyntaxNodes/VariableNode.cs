using System.Collections.Generic;

namespace Lab5.SyntaxNodes
{
    public class VariableNode : ITraversable
    {
        private string name;
        
        public double Traverse()
        {
            /*if (!node.Any()) return _variables[variableName];

            try
            {
                _variables[variableName] = Traverse(node.GetChild(0));
            }
            catch (KeyNotFoundException)
            {
                _variables.Add(variableName, Traverse(node.GetChild(0)));
            }
            */ 
            return SISharpInterpreter.Storage.Variables[name];
        }
    }
}
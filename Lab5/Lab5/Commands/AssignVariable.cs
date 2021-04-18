namespace Lab5.Commands
{
    public class AssignVariable<T> : IVisitable,IParent
    {

        private string _name;
        private T _value;

        public AssignVariable(string name,T value)
        {
            this._value = value;
            this._name = name;
        }

        public IVisitable Next { get; set; }
        
        public void Visit()
        {
            VariablePool.Instance.SetValue(_name,_value);
        }

    }
}
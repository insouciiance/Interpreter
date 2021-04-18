namespace Lab5.Commands
{
    public class AssignVariable<T> : IVisitable,IParent
    {

        private string _name;
        private T _value;

        public AssignVariable(string name,T value)
        {
            _value = value;
            _name = name;
        }

        public IVisitable Next { get; set; }
        
        public void Visit()
        {
            VariablePool.Instance.SetValue(_name,_value);
        }

    }
}
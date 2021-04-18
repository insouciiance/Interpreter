namespace Lab5.Commands
{
    public class AssignVariable<T> : IVisitable,IParent
    {

        private string name;
        private T value;

        public AssignVariable(string name,T value)
        {
            this.value = value;
            this.name = name;
        }

        public IVisitable Next { get; set; }
        
        public void Visit()
        {
            VariablePool.Instance.SetValue(name,value);
        }

    }
}
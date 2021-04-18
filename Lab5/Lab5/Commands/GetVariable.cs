namespace Lab5.Commands
{
    public class GetVariable<T> : IVisitable<T>
    {
        private string _varName;

        public GetVariable(string varName)
        {
            _varName = varName;
        }
        
        public T Visit()
        {
            return VariablePool.Instance.GetValue<T>(_varName);
        }
    }
}
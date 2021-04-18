namespace Lab5.Commands
{
    public class GetConstantValue<T> : IVisitable<T>
    {
        private T _value;

        public GetConstantValue(T value)
        {
            _value = value;
        }

        public T Visit()
        {
            return _value;
        }
    }
}
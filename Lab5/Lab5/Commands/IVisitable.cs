namespace Lab5.Commands
{
    public interface IVisitable
    {
        void Visit();
    }
    
    public interface IVisitable<T>
    {
        T Visit();
    }
}
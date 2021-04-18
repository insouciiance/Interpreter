namespace Lab5.Commands
{
    public interface IParent
    {
        IVisitable Next { get; set; }
    }
}
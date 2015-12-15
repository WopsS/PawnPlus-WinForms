namespace PawnPlus.Core.Extensibility
{
    public interface IPlugin
    {
        string Author { get; }

        string Description { get; }

        string Name { get; }
    }
}

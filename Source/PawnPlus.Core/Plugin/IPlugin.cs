namespace PawnPlus.Core
{
    public interface IPlugin
    {
        string Author { get; }

        string Description { get; }

        string Name { get; }
    }
}

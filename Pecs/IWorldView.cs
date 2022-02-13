namespace Pecs
{
    public interface IWorldView
    {
        ComponentStore<T> GetStore<T>();
    }
}

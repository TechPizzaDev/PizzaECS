namespace Pecs
{
    public interface IWorldView
    {
        DataStore<T> GetStore<T>()
            where T : struct, IElement<T>;
    }
}

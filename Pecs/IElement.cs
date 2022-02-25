
namespace Pecs
{
    public interface IElement
    {
        Entity Entity { get; }
    }

    public interface IElement<T> : IElement
        where T : struct, IElement<T>
    {
        Root<T> Root { get; }

        static abstract Root<T> CreateRoot();

        static abstract T Create(Entity entity, Root<T> root);
    }
}

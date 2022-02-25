
namespace Pecs
{
    public sealed class RefRoot<T> : Root<T>
        where T : struct, IElement<T>
    {
        private Entity[] _entities;

        public Root<T> Root { get; }

        public RefRoot()
        {
            Root = T.CreateRoot();
        }

        public T Value(Entity entity)
        {
            return T.Create(entity, Root);
        }

        public ref Entity Target(Entity entity)
        {
            return ref _entities[EntityToIndex(entity)];
        }
    }
}

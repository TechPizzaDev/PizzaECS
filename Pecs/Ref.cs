
namespace Pecs
{
    public readonly struct Ref<T>
        where T : struct, IElement<T>
    {
        public Entity Source { get; }
        public RefRoot<T> Root { get; }

        public ref Entity Target => ref Root.Target(Source);

        public T Value => Root.Value(Target);

        public Ref(Entity source, RefRoot<T> root)
        {
            Source = source;
            Root = root;
        }
    }
}

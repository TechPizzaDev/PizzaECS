
namespace Pecs
{
    public readonly struct Entity
    {
        public ulong Id { get; }

        public Entity(ulong id)
        {
            Id = id;
        }
    }
}

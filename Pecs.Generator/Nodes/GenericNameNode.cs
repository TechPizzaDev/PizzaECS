using System.Collections.Immutable;

namespace Pecs.Generator
{
    public sealed class GenericNameNode : SimpleNameNode
    {
        public ImmutableArray<TypeNode> TypeArgumentList { get; init; } =
            ImmutableArray<TypeNode>.Empty;

        public GenericNameNode(string identifier) : base(identifier)
        {
        }
    }
}
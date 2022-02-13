using System.Collections.Immutable;

namespace Pecs.Generator
{
    public sealed class AttributeListNode : Node
    {
        public AttributeTargetSpecifierNode? Target { get; }

        public ImmutableArray<AttributeNode> Attributes { get; init; } =
            ImmutableArray<AttributeNode>.Empty;
    }
}
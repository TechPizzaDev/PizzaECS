using System;
using System.Collections.Immutable;

namespace Pecs.Generator
{
    public sealed class AttributeNode : Node
    {
        public SimpleNameNode Name { get; }

        public ImmutableArray<AttributeArgumentNode> ArgumentList { get; init; } =
            ImmutableArray<AttributeArgumentNode>.Empty;

        public AttributeNode(SimpleNameNode name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
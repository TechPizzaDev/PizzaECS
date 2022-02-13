using System;
using System.Collections.Immutable;

namespace Pecs.Generator
{
    public sealed class TypeParameterNode : Node
    {
        public string? VarianceKeyword { get; init; }
        public string Identifier { get; }

        public ImmutableArray<AttributeListNode> AttributeLists { get; init; } = 
            ImmutableArray<AttributeListNode>.Empty;

        public TypeParameterNode(string identifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }
    }
}
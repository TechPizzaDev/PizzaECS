using System;
using System.Collections.Immutable;

namespace Pecs.Generator
{
    public sealed class TypeParameterConstraintClauseNode
    {
        public IdentifierNameNode Name { get; }
     
        public ImmutableArray<TypeParameterConstraintNode> Constraints { get; init; } = 
            ImmutableArray<TypeParameterConstraintNode>.Empty;

        public TypeParameterConstraintClauseNode(IdentifierNameNode name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
using System;
using System.Collections.Immutable;

namespace Pecs.Generator
{
    public abstract class BaseTypeDeclarationNode : MemberDeclarationNode
    {
        public string Identifier { get; }
        
        public ImmutableArray<TypeNode> BaseTypes { get; init; } = 
            ImmutableArray<TypeNode>.Empty;

        public BaseTypeDeclarationNode(string identifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }
    }
}
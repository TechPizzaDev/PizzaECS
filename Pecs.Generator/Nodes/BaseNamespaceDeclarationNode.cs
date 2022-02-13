using System;
using System.Collections.Immutable;

namespace Pecs.Generator
{
    public abstract class BaseNamespaceDeclarationNode : MemberDeclarationNode
    {
        public NameNode Name { get; }

        public ImmutableArray<UsingDirectiveNode> Usings { get; init; } =
            ImmutableArray<UsingDirectiveNode>.Empty;

        public ImmutableArray<MemberDeclarationNode> Members { get; init; } = 
            ImmutableArray<MemberDeclarationNode>.Empty;

        public BaseNamespaceDeclarationNode(NameNode name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
using System.Collections.Immutable;

namespace Pecs.Generator
{
    public abstract class MemberDeclarationNode : Node
    {
        public ImmutableArray<AttributeListNode> AttributeLists { get; init; } = 
            ImmutableArray<AttributeListNode>.Empty;

        public ImmutableArray<string> Modifiers { get; init; } =
            ImmutableArray<string>.Empty;
    }
}
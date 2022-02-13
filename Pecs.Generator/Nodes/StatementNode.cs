using System.Collections.Immutable;

namespace Pecs.Generator
{
    public abstract class StatementNode : Node
    {
        public ImmutableArray<AttributeListNode> AttributeLists { get; init; }
    }
}
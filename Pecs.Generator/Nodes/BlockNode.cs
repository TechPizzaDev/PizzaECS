using System.Collections.Immutable;

namespace Pecs.Generator
{
    public sealed class BlockNode : StatementNode
    {
        public ImmutableArray<StatementNode> Statements { get; init; } = 
            ImmutableArray<StatementNode>.Empty;
    }
}
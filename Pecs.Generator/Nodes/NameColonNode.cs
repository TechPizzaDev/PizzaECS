using System;

namespace Pecs.Generator
{
    public sealed class NameColonNode : BaseExpressionColonNode
    {
        public IdentifierNameNode Name { get; }
        public override ExpressionNode Expression => Name;

        public NameColonNode(IdentifierNameNode name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
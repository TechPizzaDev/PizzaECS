using System;

namespace Pecs.Generator
{
    public sealed class NameEqualsNode : Node
    {
        public IdentifierNameNode Name { get; }

        public NameEqualsNode(IdentifierNameNode name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
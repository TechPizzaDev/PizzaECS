using System;

namespace Pecs.Generator
{
    public sealed class UsingDirectiveNode : Node
    {
        public NameNode Name { get; }
        public NameEqualsNode? Alias { get; init; }

        public UsingDirectiveNode(NameNode name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
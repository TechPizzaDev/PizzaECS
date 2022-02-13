using System;

namespace Pecs.Generator
{
    public sealed class AttributeTargetSpecifierNode : Node
    {
        public string Identifier { get; }

        public AttributeTargetSpecifierNode(string identifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }
    }
}
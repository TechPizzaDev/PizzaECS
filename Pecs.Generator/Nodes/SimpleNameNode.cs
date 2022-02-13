using System;

namespace Pecs.Generator
{
    public abstract class SimpleNameNode : NameNode
    {
        public string Identifier { get; }

        public SimpleNameNode(string identifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }
    }
}
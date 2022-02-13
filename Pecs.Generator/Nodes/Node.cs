using System.IO;

namespace Pecs.Generator
{
    public abstract class Node
    {
        public virtual void WriteTo(TextWriter textWriter)
        {

        }

        public override string ToString()
        {
            using StringWriter writer = new();
            WriteTo(writer);
            return writer.ToString();
        }
    }
}
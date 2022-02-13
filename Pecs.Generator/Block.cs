using System.Collections.Generic;
using System.Text;

namespace Pecs.Generator
{
    public class Block
    {
        public List<object> OutsideContents { get; }
        public List<object> Contents { get; }

        public Block()
        {
            OutsideContents = new List<object>();
            Contents = new List<object>();
        }

        private Block Add(object text)
        {
            Contents.Add(text);
            return this;
        }

        public Block AddLine()
        {
            return Add("\n");
        }

        public Block AddLine(object text)
        {
            return Add(text).AddLine();
        }

        private Block AddOutside(object text)
        {
            OutsideContents.Add(text);
            return this;
        }

        public Block AddOutsideLine()
        {
            return AddOutside("\n");
        }

        public Block AddOutsideLine(object text)
        {
            return AddOutside(text).AddOutsideLine();
        }

        public Block AddBlock()
        {
            Block block = new();
            Contents.Add(block);
            return block;
        }

        public void WriteTo(StringBuilder builder, bool includeBrackets = true)
        {
            WriteTo(builder, "", includeBrackets);
        }

        private void WriteTo(StringBuilder builder, string indent, bool includeBrackets)
        {
            foreach (object outside in OutsideContents)
            {
                builder.Append(indent).Append(outside);
            }

            if (includeBrackets)
            {
                builder.Append(indent).AppendLine("{");
            }

            string innerIndent = indent + "    ";
            foreach (object content in Contents)
            {
                if (content is Block block)
                    block.WriteTo(builder, innerIndent, true);
                else
                    builder.Append(innerIndent).Append(content);
            }

            if (includeBrackets)
            {
                builder.Append(indent).AppendLine("}");
            }
        }
    }
}

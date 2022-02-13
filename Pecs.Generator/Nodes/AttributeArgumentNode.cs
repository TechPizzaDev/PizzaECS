using System;

namespace Pecs.Generator
{
    public sealed class AttributeArgumentNode
    {
        public NameEqualsNode? NameEquals { get; init; }
        public NameColonNode? NameColon { get; init; }
        public ExpressionNode Expression { get; }

        public AttributeArgumentNode(ExpressionNode expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }
    }
}
namespace Pecs.Generator
{
    public abstract class BaseExpressionColonNode : Node
    {
        public abstract ExpressionNode Expression { get; }
    }
}
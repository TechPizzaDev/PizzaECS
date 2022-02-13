using System.Collections.Immutable;

namespace Pecs.Generator
{
    public abstract class TypeDeclarationNode : BaseTypeDeclarationNode
    {
        public ImmutableArray<TypeParameterNode> TypeParameterList { get; init; } =
            ImmutableArray<TypeParameterNode>.Empty;

        public ImmutableArray<TypeParameterConstraintClauseNode> ConstraintClauses { get; init; } = 
            ImmutableArray<TypeParameterConstraintClauseNode>.Empty;

        public ImmutableArray<MemberDeclarationNode> Members { get; init; } = 
            ImmutableArray<MemberDeclarationNode>.Empty;

        public TypeDeclarationNode(string identifier) : base(identifier)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Pecs.Generator
{
    [Generator]
    public class PecsGenerator : IIncrementalGenerator
    {
        public const string aggressiveInline = "[MethodImpl(MethodImplOptions.AggressiveInlining)]";

        public static string WorldTypeName { get; } = "World";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
#if DEBUGANALYZER
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif

            IncrementalValueProvider<ImmutableArray<int>> genericCounts = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (node, _) =>
                    {
                        if (node is GenericNameSyntax genericNameSyntax)
                        {
                            if (genericNameSyntax.Identifier.ToString() == WorldTypeName)
                            {
                                return true;
                            }
                        }
                        return false;
                    },
                    transform: static (ctx, _) =>
                    {
                        return ((GenericNameSyntax)ctx.Node).TypeArgumentList.Arguments.Count;
                    }).Collect();

            context.RegisterSourceOutput(genericCounts, static (ctx, counts) =>
            {
                // Deduplicate the counts with a set.
                SortedSet<int> argumentCounts = new(counts);

                StringBuilder builder = new();

                foreach (int argumentCount in argumentCounts)
                {
                    if (argumentCount == 0)
                    {
                        continue;
                    }

                    Block worldGenericsBlock = new Block()
                        .AddOutsideLine("using System.Runtime.CompilerServices;")
                        .AddOutsideLine()
                        .AddOutsideLine("namespace Pecs");

                    List<string> genericTypeNames = new();
                    for (int i = 0; i < argumentCount; i++)
                    {
                        genericTypeNames.Add("T" + (i + 1));
                    }

                    Block genericsBlock = worldGenericsBlock.AddBlock();
                    worldGenericsBlock.AddLine();

                    CreateWorldGeneric(genericTypeNames, genericsBlock);

                    CreateWorldGeneric(genericTypeNames);

                    builder.Clear();
                    worldGenericsBlock.WriteTo(builder);

                    ctx.AddSource($"World{{T{argumentCount}}}.g", builder.ToString());
                }
            });
        }

        public static void CreateWorldGeneric(List<string> genericTypeNames, Block block)
        {
            StringBuilder genericTypeList = new();
            {
                genericTypeList.Append("<");
                for (int i = 0; i < genericTypeNames.Count - 1; i++)
                {
                    genericTypeList.Append(genericTypeNames[i]).Append(", ");
                }
                genericTypeList.Append(genericTypeNames[genericTypeNames.Count - 1]);
                genericTypeList.Append(">");
            }

            List<string> fieldNames = genericTypeNames.Select(g => "_store" + g).ToList();

            block.AddOutsideLine($"public sealed class World{genericTypeList} : World");

            // append fields in class
            for (int i = 0; i < genericTypeNames.Count; i++)
            {
                block.AddLine($"private ComponentStore<{genericTypeNames[i]}> {fieldNames[i]};");
            }
            block.AddLine();

            Block constructorBlock = block.AddBlock()
                .AddOutsideLine("public World()");

            block.AddLine();

            Block getStoreBlock = block.AddBlock()
                .AddOutsideLine(aggressiveInline)
                .AddOutsideLine("public override ComponentStore<T> GetStore<T>()");

            block.AddLine();

            Block getComponentBlock = block.AddBlock()
                .AddOutsideLine(aggressiveInline)
                .AddOutsideLine("public override ref T GetComponent<T>(Entity entity)");

            for (int i = 0; i < genericTypeNames.Count; i++)
            {
                string genericName = genericTypeNames[i];
                string fieldName = fieldNames[i];

                // append field construction in constructor
                constructorBlock.AddLine($"{fieldName} = new ComponentStore<{genericName}>();");

                // append AddStore in constructor
                constructorBlock.AddLine($"AddStore({fieldName});");

                string ifBlockCheck = $"if (typeof(T) == typeof({genericName}))";
                string genericStoreAccess = $"Unsafe.As<ComponentStore<T>>({fieldName})";

                // create type check in GetStore
                Block storeIfBlock = getStoreBlock.AddBlock()
                    .AddOutsideLine(ifBlockCheck);

                // create type check in GetComponent
                Block componentIfBlock = getComponentBlock.AddBlock()
                    .AddOutsideLine(ifBlockCheck);

                // return correct store by field
                storeIfBlock.AddLine($"return {genericStoreAccess};");
            }

            getStoreBlock.AddLine($"return base.GetStore<T>();");
            getComponentBlock.AddLine($"return ref GetStore<T>().GetComponent(entity);");
        }

        public static void CreateWorldGeneric(List<string> genericTypeNames)
        {
            var typeArgumentList = genericTypeNames.Select(name => new IdentifierNameNode(name)).ToImmutableArray<TypeNode>();
            GenericNameNode worldName = new("World")
            {
                TypeArgumentList = typeArgumentList
            };
        }
    }
}
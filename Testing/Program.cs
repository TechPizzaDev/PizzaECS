using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Pecs;

namespace Testing
{
    public abstract class ReferenceRoot<T> : Root
        where T : IElement
    {
        private Entity[] _entities;
        
        public abstract T Value(Entity entity);

        public ref Entity Target(Entity entity)
        {
            return ref _entities[EntityToIndex(entity)];
        }
    }

    public sealed class SkeletonReferenceRoot : ReferenceRoot<Skeleton>
    {
        private SkeletonRoot _root;

        public override Skeleton Value(Entity entity) => new(entity, _root);
    }

    public sealed class SkeletonRoot : Root
    {
        private Float3Root r_Position;
        private Float3Root r_Velocity;
        private SkeletonReferenceRoot r_Parent;

        public Float3 Position(Entity entity) => new(entity, r_Position);
        public Float3 Velocity(Entity entity) => new(entity, r_Velocity);
        public Reference<Skeleton> Parent(Entity entity) => new(entity, r_Parent);
    }

    public readonly struct Reference<T>
        where T : IElement
    {
        public Entity Source { get; }
        public ReferenceRoot<T> Root { get; }

        public ref Entity Target => ref Root.Target(Source);
        public T Value => Root.Value(Target);

        public Reference(Entity source, ReferenceRoot<T> root)
        {
            Source = source;
            Root = root;
        }
    }

    public partial struct Skeleton
    {
        public Entity Entity { get; }
        public SkeletonRoot Root { get; }

        Root IElement.Root => Root;

        public Skeleton(Entity entity, SkeletonRoot root)
        {
            Entity = entity;
            Root = root;
        }

        public partial Float3 Position() => Root.Position(Entity);
        public partial Float3 Velocity() => Root.Velocity(Entity);
        public partial Reference<Skeleton> Parent() => Root.Parent(Entity);
    }

    public sealed class Float3Root : Root
    {
        private float[] a_X;
        private float[] a_Y;
        private float[] a_Z;

        public ref float X(Entity entity) => ref a_X[EntityToIndex(entity)];
        public ref float Y(Entity entity) => ref a_Y[EntityToIndex(entity)];
        public ref float Z(Entity entity) => ref a_Z[EntityToIndex(entity)];
    }

    public partial struct Float3
    {
        public Entity Entity { get; }
        public Float3Root Root { get; }

        Root IElement.Root => Root;

        public Float3(Entity entity, Float3Root root)
        {
            Entity = entity;
            Root = root;
        }

        public partial ref float X() => ref Root.X(Entity);
        public partial ref float Y() => ref Root.Y(Entity);
        public partial ref float Z() => ref Root.Z(Entity);
    }

    public partial struct Float3 : IElement
    {
        public partial ref float X();
        public partial ref float Y();
        public partial ref float Z();
    }

    public partial struct Skeleton : IElement
    {
        public partial Float3 Position();
        public partial Float3 Velocity();
        public partial Reference<Skeleton> Parent();
    }

    class Program
    {
        static void Main(string[] args)
        {
            World<int> world = new World<int>();

            World<int> world2 = new World<int>();

            World<int> world3 = new World<int>();

            var world4 = new World<int, float>();

            var world5 = new World<int, float, double>();

            ComponentStore<int> store1 = world.GetStore<int>();

            ComponentStore<short> store2 = world.GetStore<short>();

            //var world2 = new World<int, short>();


        }
    }
}

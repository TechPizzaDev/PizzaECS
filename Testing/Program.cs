using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Pecs;

namespace Testing
{
    public partial struct Skeleton : IElement<Skeleton>
    {
        public partial Float3 Position() => Root.Position(Entity);
        public partial Float3 Velocity() => Root.Velocity(Entity);
        public partial Ref<Skeleton> Parent() => Root.Parent(Entity);

        public Entity Entity { get; }
        private TypedRoot Root { get; }
        Root<Skeleton> IElement<Skeleton>.Root => Root;

        private Skeleton(Entity entity, TypedRoot root)
        {
            Entity = entity;
            Root = root;
        }

        public static Skeleton Create(Entity entity, Root<Skeleton> root)
        {
            return new Skeleton(entity, (TypedRoot)root);
        }

        public static Root<Skeleton> CreateRoot()
        {
            return new TypedRoot();
        }

        private sealed class TypedRoot : Root<Skeleton>
        {
            private Root<Float3> r_Position;
            private Root<Float3> r_Velocity;
            private RefRoot<Skeleton> r_Parent;

            public Float3 Position(Entity entity) => Float3.Create(entity, r_Position);
            public Float3 Velocity(Entity entity) => Float3.Create(entity, r_Velocity);
            public Ref<Skeleton> Parent(Entity entity) => new(entity, r_Parent);
        }
    }

    public partial struct Float3 : IElement<Float3>
    {
        public partial ref float X() => ref Root.X(Entity);
        public partial ref float Y() => ref Root.Y(Entity);
        public partial ref float Z() => ref Root.Z(Entity);

        public Entity Entity { get; }
        private TypedRoot Root { get; }
        Root<Float3> IElement<Float3>.Root => Root;

        private Float3(Entity entity, TypedRoot root)
        {
            Entity = entity;
            Root = root;
        }

        public static Float3 Create(Entity entity, Root<Float3> root)
        {
            return new Float3(entity, (TypedRoot)root);
        }

        public static Root<Float3> CreateRoot()
        {
            return new TypedRoot();
        }

        private sealed class TypedRoot : Root<Float3>
        {
            private float[] a_X;
            private float[] a_Y;
            private float[] a_Z;

            public ref float X(Entity entity) => ref a_X[EntityToIndex(entity)];
            public ref float Y(Entity entity) => ref a_Y[EntityToIndex(entity)];
            public ref float Z(Entity entity) => ref a_Z[EntityToIndex(entity)];
        }
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
        public partial Ref<Skeleton> Parent();
    }

    class Program
    {
        static void Main(string[] args)
        {
            World world = new();
            
            Entity entity = new();

            Skeleton skeleton = world.GetStore<Skeleton>().Create(entity);

            //World<int> world = new World<int>();
            //
            //World<int> world2 = new World<int>();
            //
            //World<int> world3 = new World<int>();
            //
            //var world4 = new World<int, float>();
            //
            //var world5 = new World<int, float, double>();
            //var world6 = new World<int, float, double>();
            //var world7 = new World<int, float, double>();
            //
            //ComponentStore<int> store1 = world.GetStore<int>();
            //
            //ComponentStore<short> store2 = world.GetStore<short>();

            //var world2 = new World<int, short>();


        }
    }
}

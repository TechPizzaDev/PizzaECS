using System;
using System.Runtime.CompilerServices;

namespace Pecs
{
    public abstract class ComponentStore
    {

    }

    public class ComponentStore<T> : ComponentStore
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T GetComponent(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Runtime.CompilerServices;

namespace Pecs
{
    public abstract class DataStore
    {

    }

    public class DataStore<T> : DataStore
        where T : struct, IElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(Entity entity)
        {
            throw new NotImplementedException();
        }

        public T Create(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}

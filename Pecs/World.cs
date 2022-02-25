using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Pecs
{
    public partial class World : IWorldView
    {
        private Dictionary<Type, DataStore> _stores = new();

        public void AddStore<T>(DataStore<T> store)
            where T : struct, IElement<T>
        {
            _stores.Add(typeof(T), store);
        }

        public virtual DataStore<T> GetStore<T>()
            where T : struct, IElement<T>
        {
            if (!_stores.TryGetValue(typeof(T), out DataStore store))
            {
                DataStore<T> genericStore = new();
                _stores.Add(typeof(T), genericStore);
                return genericStore;
            }
            return Unsafe.As<DataStore<T>>(store);
        }

        public virtual T GetComponent<T>(Entity entity)
            where T : struct, IElement<T>
        {
            if (_stores.TryGetValue(typeof(T), out DataStore store))
            {
                return Unsafe.As<DataStore<T>>(store).Get(entity);
            }
            return default;
        }
    }
}

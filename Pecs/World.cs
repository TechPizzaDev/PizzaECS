using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Pecs
{
    public partial class World : IWorldView
    {
        private Dictionary<Type, ComponentStore> _stores = new();

        public void AddStore<T>(ComponentStore<T> store)
        {
            _stores.Add(typeof(T), store);
        }

        public virtual ComponentStore<T> GetStore<T>()
        {
            if (!_stores.TryGetValue(typeof(T), out ComponentStore store))
            {
                ComponentStore<T> genericStore = new();
                _stores.Add(typeof(T), genericStore);
                return genericStore;
            }
            return Unsafe.As<ComponentStore<T>>(store);
        }

        public virtual ref T GetComponent<T>(Entity entity)
        {
            if (_stores.TryGetValue(typeof(T), out ComponentStore store))
            {
                return ref Unsafe.As<ComponentStore<T>>(store).GetComponent(entity);
            }
            return ref Unsafe.NullRef<T>();
        }
    }
}

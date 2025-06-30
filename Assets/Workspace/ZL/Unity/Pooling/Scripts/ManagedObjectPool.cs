using System;

using System.Collections.Generic;

using System.Linq;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public sealed class ManagedObjectPool<TKey> : ManagedObjectPool<TKey, ManagedPooledObject<TKey>>
    {

    }

    [Serializable]

    public class ManagedObjectPool<TKey, TClone> : ObjectPool<TClone>

        where TClone : ManagedPooledObject<TKey>
    {
        private readonly Dictionary<TKey, TClone> clones = new();

        public TClone this[TKey key]
        {
            get => clones[key];
        }

        public bool TryClone(TKey key, out TClone clone)
        {
            if (clones.ContainsKey(key) == true)
            {
                clone = clones[key];

                return false;
            }

            clone = Clone();

            clone.Key = key;

            clones.Add(key, clone);

            return true;
        }

        public override void Collect(TClone clone)
        {
            base.Collect(clone);

            clones.Remove(clone.Key);
        }

        public void CollectAll()
        {
            foreach (var clone in clones.Values.ToArray())
            {
                clone.Disappear();
            }
        }
    }
}
using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Pooled Object")]

    public class PooledObject : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private float lifeTime = -1f;

        public float LifeTime
        {
            set => lifeTime = value;
        }

        public event Action OnDisappearedAction = null;

        private event Action OnCollectedAction = null;

        public static TClone Instantiate<TClone>(ObjectPool<TClone> objectPool)

            where TClone : PooledObject
        {
            var clone = Instantiate(objectPool.Prefab, objectPool.Parent);

            clone.OnCollectedAction += () => objectPool.Collect(clone);

            return clone;
        }

        public virtual void Appear()
        {
            gameObject.SetActive(true);

            OnAppeared();
        }

        public virtual void OnAppeared()
        {
            if (lifeTime != -1f)
            {
                Invoke(nameof(Disappear), lifeTime);

                lifeTime = -1f;
            }
        }

        public virtual void Disappear()
        {
            OnDisappeared();
        }

        public virtual void OnDisappeared()
        {
            if (OnDisappearedAction != null)
            {
                OnDisappearedAction.Invoke();

                OnDisappearedAction = null;
            }

            OnCollectedAction?.Invoke();

            gameObject.SetActive(false);
        }
    }
}
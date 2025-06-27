using UnityEngine;

namespace ZL.Unity.Unimo
{
    public abstract class Item : SpawnedObject
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        protected Collider mainCollider = null;

        public Collider MainCollider
        {
            get => mainCollider;
        }

        public override void OnAppeared()
        {
            mainCollider.enabled = true;

            base.OnAppeared();
        }

        public override void Disappear()
        {
            mainCollider.enabled = false;

            base.Disappear();
        }

        public abstract void GetItem<TMonoBehaviour>(TMonoBehaviour getter)
            
            where TMonoBehaviour : MonoBehaviour;
    }
}
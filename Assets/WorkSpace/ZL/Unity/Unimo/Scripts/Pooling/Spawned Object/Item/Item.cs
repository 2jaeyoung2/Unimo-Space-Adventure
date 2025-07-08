using UnityEngine;

namespace ZL.Unity.Unimo
{
    public abstract class Item : SpawnedObject
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        protected Collider mainCollider = null;

        public Collider MainCollider
        {
            get => mainCollider;
        }

        public override void OnAppeared()
        {
            base.OnAppeared();

            mainCollider.enabled = true;
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
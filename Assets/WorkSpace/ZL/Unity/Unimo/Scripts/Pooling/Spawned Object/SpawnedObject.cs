using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class SpawnedObject : PooledObject
    {
        [Line]

        [GetComponentInChildren]

        [Essential]

        [ReadOnly(true)]
        
        [UsingCustomProperty]

        [SerializeField]

        protected AnimatorGroup animatorGroup = null;

        [Space]

        [SerializeField]

        protected Transform destination = null;

        public virtual Transform Destination
        {
            set => destination = value;
        }

        [Space]

        [SerializeField]

        protected float rotationSpeedMultiplier = 1f;

        public float RotationSpeedMultiplier
        {
            set => rotationSpeedMultiplier = value;
        }

        [SerializeField]

        protected float movementSpeedMultiplier = 1f;

        public virtual float MovementSpeedMultiplier
        {
            get => movementSpeedMultiplier;

            set => movementSpeedMultiplier = value;
        }

        [SerializeField]

        private float despawnDistance = -1f;

        public float DespawnDistance
        {
            set => despawnDistance = value;
        }

        private Vector3 spawnPosition = Vector3.zero;

        public Vector3 SpawnPosition
        {
            set => spawnPosition = value;
        }

        public override void Appear()
        {
            gameObject.SetActive(true);

            spawnPosition = transform.position;
        }

        public override void Disappear()
        {
            CancelInvoke(nameof(Disappear));

            OnDisappear();
        }

        protected virtual void OnDisappear()
        {
            animatorGroup.SetTrigger("Disappear");
        }

        public override void OnDisappeared()
        {
            animatorGroup.Rebind();

            destination = null;

            rotationSpeedMultiplier = 1f;

            movementSpeedMultiplier = 1f;

            despawnDistance = -1f;

            base.OnDisappeared();
        }

        protected virtual void CheckDespawnCondition()
        {
            if (despawnDistance == -1f)
            {
                return;
            }

            if (IsWithinRange(spawnPosition, despawnDistance) == true)
            {
                return;
            }

            Disappear();
        }

        protected bool IsWithinRange(Vector3 position, float distance)
        {
            return transform.position.DistanceTo(position, Axis.Y) <= distance;
        }
    }
}
using UnityEngine;

namespace ZL.Unity.Combat
{
    public interface IDamageable
    {
        public Collider MainCollider { get; }

        public float CurrentHealth { get; }

        public void TakeDamage(float damage, Vector3 contact);
    }
}
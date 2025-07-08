using UnityEngine;

using ZL.Unity.Combat;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy Projectile (Spawned)")]

    public sealed class EnemyProjectile : Enemy, IDamager
    {
        private Transform muzzle = null;

        public Transform Muzzle
        {
            set => muzzle = value;
        }

        private void LateUpdate()
        {
            if (muzzle != null)
            {
                transform.SetPositionAndRotation(muzzle);
            }
        }

        public override void OnAppeared()
        {
            base.OnAppeared();

            muzzle = null;
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);

            Disappear();
        }
    }
}
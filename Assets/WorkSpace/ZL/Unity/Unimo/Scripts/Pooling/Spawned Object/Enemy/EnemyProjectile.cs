using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy Projectile (Spawned)")]

    public sealed class EnemyProjectile : Enemy, IDamager
    {
        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);

            Disappear();
        }
    }
}
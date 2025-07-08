using UnityEngine;

using ZL.Unity.Combat;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 4 (Spawned)")]

    public sealed class Monster4 : Enemy, IDamager
    {
        [Space]

        [SerializeField]

        private float chargeDashTime = 0f;

        public override void Appear()
        {
            base.Appear();

            movementSpeed = 0f;
        }

        public override void OnAppeared()
        {
            base.OnAppeared();

            animatorGroup.SetFloat(nameof(chargeDashTime), chargeDashTime);

            animatorGroup.SetTrigger("Dash");
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        public void Dash()
        {
            movementSpeed = enemyData.MovementSpeed;
        }
    }
}
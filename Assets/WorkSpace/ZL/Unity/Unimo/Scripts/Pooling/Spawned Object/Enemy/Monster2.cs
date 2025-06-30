using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 2 (Spawned)")]

    public sealed class Monster2 : Enemy, IDamager
    {
        [Line]

        [UsingCustomProperty]

        [SerializeField]

        private float stopDistance = 0f;

        [Space]

        [SerializeField]

        private float attackCooldownTime = 0f;

        [SerializeField]

        private float attackRange = 0f;

        private float attackCooldownTimer = 0f;

        [Space]

        [SerializeField]

        private string projectileName = "";

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private Transform muzzle = null;

        private void Update()
        {
            if (isStoped == true)
            {
                return;
            }

            if (attackCooldownTimer > 0f)
            {
                attackCooldownTimer -= Time.deltaTime;

                return;
            }

            if (IsWithinRange(EnemyManager.Instance.SkillTarget.position, attackRange) == false)
            {
                return;
            }

            attackCooldownTimer = attackCooldownTime;

            animatorGroup.SetTrigger("Attack");
        }

        public override void OnDisappeared()
        {
            attackCooldownTimer = 0f;

            base.OnDisappeared();
        }

        protected override void Movement()
        {
            if (IsWithinRange(destination.position, stopDistance) == true)
            {
                movementSpeed = 0f;
            }

            else
            {
                movementSpeed = enemyData.MovementSpeed;
            }

            base.Movement();
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        public void Shoot()
        {
            var enemyProjectile = ObjectPoolManager.Instance.Clone<EnemyProjectile>(projectileName);

            enemyProjectile.Muzzle = muzzle;

            enemyProjectile.Appear();
        }
    }
}
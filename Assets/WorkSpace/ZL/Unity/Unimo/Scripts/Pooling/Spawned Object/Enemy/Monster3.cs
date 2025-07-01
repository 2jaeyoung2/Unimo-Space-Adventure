using UnityEngine;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 3 (Spawned)")]

    public sealed class Monster3 : Enemy, IDamager
    {
        [Space]

        [SerializeField]

        private float dashSpeedMultiplier = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private ArcedDetector detector = null;

        private bool isDashing = false;

        private void Update()
        {
            if (isStoped == true)
            {
                return;
            }

            if (detector.Detect(EnemyManager.Instance.SkillTarget) == true)
            {
                detector.enabled = false;

                movementSpeed = 0f;

                animatorGroup.SetTrigger("Encounter");

                CancelInvoke(nameof(Disappear));
            }
        }

        public override void OnAppeared()
        {
            base.OnAppeared();

            detector.enabled = true;
        }

        public override void Disappear()
        {
            detector.enabled = false;

            base.Disappear();
        }

        protected override void OnDisappear()
        {
            animatorGroup.Rebind();

            if (isDashing == true)
            {
                animatorGroup.SetTrigger("DashToDisappear");
            }

            else
            {
                animatorGroup.SetTrigger("Disappear");
            }
        }

        public override void OnDisappeared()
        {
            isDashing = false;

            base.OnDisappeared();
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        public void Dash()
        {
            movementSpeed = enemyData.MovementSpeed * dashSpeedMultiplier;
        }
    }
}
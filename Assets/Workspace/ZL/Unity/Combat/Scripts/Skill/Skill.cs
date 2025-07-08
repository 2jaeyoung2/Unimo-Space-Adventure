using System.Collections;

using UnityEngine;

namespace ZL.Unity.Combat
{
    public abstract class Skill<TSkillUser, TSkillData>

        where TSkillUser : class

        where TSkillData : class, ISkillData
    {
        [Essential]

        [UsingCustomProperty]

        [SerializeField]

        protected TSkillUser skillUser = null;

        [Essential]

        [UsingCustomProperty]

        [SerializeField]

        protected TSkillData skillData = null;

        [Space]

        [SerializeField]

        protected float initialCooldownTimer = -1f;

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        protected float cooldownTimer = 0f;

        public virtual void Construct()
        {
            if (initialCooldownTimer == -1f)
            {
                SetCooldownTimer();

                return;
            }

            cooldownTimer = initialCooldownTimer;
        }

        public void SetCooldownTimer()
        {
            cooldownTimer = skillData.CooldownTime;
        }

        public void Cooldown()
        {
            cooldownTimer = 0f;
        }

        public virtual void Cooldown(float time)
        {
            cooldownTimer -= time;

            if (cooldownTimer < 0f)
            {
                cooldownTimer = 0f;
            }
        }

        public virtual float GetWeight()
        {
            if (cooldownTimer > 0f)
            {
                return 0f;
            }

            return skillData.Weight;
        }

        public abstract IEnumerator Routine();

        public virtual void Reset()
        {
            Construct();
        }
    }
}
using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

namespace ZL.Unity.Unimo
{
    public sealed class SkillSequence<TSkillUser>

        where TSkillUser : MonoBehaviour
    {
        private readonly Skill<TSkillUser>[] skills = null;

        public SkillSequence(params Skill<TSkillUser>[] skills)
        {
            this.skills = skills;

            for (int i = 0; i < skills.Length; ++i)
            {
                skills[i].Construct();
            }
        }

        public IEnumerator Routine()
        {
            Cooldown(Time.fixedDeltaTime);

            if (MathfEx.CDF(skills, GetWeight, out int index) == true)
            {
                skills[index].SetCooldownTimer();

                yield return skills[index].Routine();
            }

            else
            {
                yield return WaitForFixedUpdateCache.Get();
            }
        }

        public void Cooldown(float time)
        {
            for (int i = 0; i < skills.Length; ++i)
            {
                skills[i].Cooldown(time);
            }
        }

        private float GetWeight(Skill<TSkillUser> skill)
        {
            return skill.GetWeight();
        }

        public void Reset()
        {
            for (int i = 0; i < skills.Length; ++i)
            {
                skills[i].Reset();
            }
        }
    }
}
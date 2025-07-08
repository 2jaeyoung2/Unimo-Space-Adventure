using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.Coroutines;

namespace ZL.Unity.Combat
{
    public sealed class SkillSequence<TSkillUser, TSkillData>

        where TSkillUser : MonoBehaviour

        where TSkillData : class, ISkillData
    {
        private List<Skill<TSkillUser, TSkillData>> skills = null;

        public SkillSequence(params Skill<TSkillUser, TSkillData>[] skills)
        {
            this.skills = new(skills.Length);

            for (int i = 0; i < skills.Length; ++i)
            {
                skills[i].Construct();

                this.skills.Add(skills[i]);
            }
        }

        public void Add(Skill<TSkillUser, TSkillData> skill)
        {
            skill.Construct();

            skills.Add(skill);
        }

        public void Remove(Skill<TSkillUser, TSkillData> skill)
        {
            skills.Remove(skill);
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
            for (int i = 0; i < skills.Count; ++i)
            {
                skills[i].Cooldown(time);
            }
        }

        private float GetWeight(Skill<TSkillUser, TSkillData> skill)
        {
            return skill.GetWeight();
        }

        public void Reset()
        {
            for (int i = 0; i < skills.Count; ++i)
            {
                skills[i].Reset();
            }
        }
    }
}
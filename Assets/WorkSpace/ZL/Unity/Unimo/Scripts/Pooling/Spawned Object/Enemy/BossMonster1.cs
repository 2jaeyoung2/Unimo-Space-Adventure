using System;

using System.Collections;

using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Coroutines;

using ZL.Unity.Phys;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Boss Monster 1 (Spawned)")]

    public sealed class BossMonster1 : Enemy, IDamager, IEnergizer
    {
        [Line]

        [Essential]

        [UsingCustomProperty]

        [SerializeField]

        private GameObject hitVFX = null;

        [Space]

        [SerializeField]

        private DashSkill dashSkill = null;

        [Space]

        [SerializeField]

        private EnergyBoltSkill energyBoltSkill = null;

        [Space]

        [SerializeField]

        private FindClosestObjectSkill findEnergySkill = null;

        private int energy = 0;

        public int Energy
        {
            get => energy;

            set => energy = value;
        }

        private SkillSequence<BossMonster1, SkillData> skillSequence = null;

        private void Awake()
        {
            skillSequence = new(dashSkill, energyBoltSkill, findEnergySkill);
        }

        public override void OnAppeared()
        {
            base.OnAppeared();

            skillSequenceRoutine = SkillSequenceRoutine();

            StartCoroutine(skillSequenceRoutine);

            EnemyUIScreen.Instance.AppearNamedEnemyHealthBar(this);
        }

        private IEnumerator skillSequenceRoutine = null;

        private IEnumerator SkillSequenceRoutine()
        {
            while (true)
            {
                yield return skillSequence.Routine();
            }
        }

        public override void Disappear()
        {
            if (skillSequenceRoutine != null)
            {
                StopCoroutine(skillSequenceRoutine);

                skillSequenceRoutine = null;
            }

            skillSequence.Reset();

            EnemyUIScreen.Instance.DisappearNamedEnemyHealthBar(this);

            base.Disappear();
        }

        public override void TakeDamage(float damage, Vector3 contact)
        {
            hitVFX.transform.LookAt(contact, Axis.Y);

            hitVFX.SetActive(true);

            base.TakeDamage(damage, contact);
        }

        protected override void Kill()
        {
            ++StageQuestList.Instance.BossKillCount;

            ScoreManager.Instance.CountBossKill();

            base.Kill();
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                var item = other.GetComponent<Item>();

                item.GetItem(this);
            }
        }

        public void GetEnergy(int value)
        {
            Energy += value;

            energyBoltSkill.Cooldown();
        }

        [Serializable]

        public sealed class DashSkill : Skill<BossMonster1, SkillData>
        {
            [Space]

            [Essential]

            [UsingCustomProperty]

            [SerializeField]

            private GameObject dashSFX = null;

            public override IEnumerator Routine()
            {
                skillUser.rotationSpeed = 0f;

                skillUser.MovementSpeedMultiplier *= skillData.Power;

                dashSFX.SetActive(true);

                yield return WaitForSecondsCache.Get(skillData.Duration);

                skillUser.rotationSpeed = skillUser.enemyData.RotationSpeed;

                skillUser.MovementSpeedMultiplier /= skillData.Power;

                dashSFX.SetActive(false);
            }
        }

        [Serializable]

        public sealed class EnergyBoltSkill : Skill<BossMonster1, SkillData>
        {
            [Space]

            [Essential]

            [UsingCustomProperty]

            [SerializeField]

            private ArcedDetector detector = null;

            [SerializeField]

            private float angle = 0f;

            [Space]

            [Essential]

            [UsingCustomProperty]

            [SerializeField]

            private Transform muzzle = null;

            [Space]

            [Essential]

            [UsingCustomProperty]

            [SerializeField]

            private string projectileName = "";

            [Essential]

            [Alias("Projectile Name (Enhanced)")]

            [UsingCustomProperty]

            [SerializeField]

            private string projectileName_Enhanced = "";

            public override void Construct()
            {
                detector.Radius = skillData.Range;

                detector.Angle = angle;

                base.Construct();
            }

            public override float GetWeight()
            {
                if (detector.Detect(EnemyManager.Instance.SkillTarget) == false)
                {
                    return 0f;
                }

                return base.GetWeight();
            }

            public override IEnumerator Routine()
            {
                EnemyProjectile projectile;

                if (skillUser.energy > 0)
                {
                    --skillUser.energy;

                    projectile = ObjectPoolManager.Instance.Clone<EnemyProjectile>(projectileName_Enhanced);
                }

                else
                {
                    projectile = ObjectPoolManager.Instance.Clone<EnemyProjectile>(projectileName);
                }

                projectile.LifeTime = skillData.Duration;

                projectile.Destination = EnemyManager.Instance.SkillTarget;

                projectile.Muzzle = muzzle;

                projectile.Appear();

                yield return WaitForEndOfFrameCache.Get();
            }
        }

        [Serializable]

        public sealed class FindClosestObjectSkill : Skill<BossMonster1, SkillData>
        {
            [Space]

            [SerializeField]

            private string targetObjectName = "";

            private Transform closestObject = null;

            public override void Cooldown(float time)
            {
                if (findClosestObjectRoutine != null)
                {
                    return;
                }

                base.Cooldown(time);
            }

            public override float GetWeight()
            {
                if (cooldownTimer > 0f)
                {
                    return 0f;
                }

                closestObject = ObjectPoolManager.Instance.FindClosestObject(skillUser.transform, targetObjectName, Axis.Y, skillData.Range);

                if (closestObject == null)
                {
                    return 0f;
                }

                return skillData.Weight;
            }

            public override IEnumerator Routine()
            {
                if (findClosestObjectRoutine == null)
                {
                    findClosestObjectRoutine = FindClosestObjectRoutine();

                    skillUser.StartCoroutine(findClosestObjectRoutine);
                }

                yield return WaitForEndOfFrameCache.Get();
            }

            private IEnumerator findClosestObjectRoutine = null;

            private IEnumerator FindClosestObjectRoutine()
            {
                skillUser.finalDestination = closestObject;

                while (true)
                {
                    yield return WaitForSecondsCache.Get(0.5f);

                    if (closestObject.gameObject.activeSelf == false)
                    {
                        break;
                    }
                }

                skillUser.finalDestination = skillUser.destination;

                findClosestObjectRoutine = null;
            }

            public override void Reset()
            {
                closestObject = null;

                if (findClosestObjectRoutine != null)
                {
                    skillUser.StopCoroutine(findClosestObjectRoutine);

                    findClosestObjectRoutine = null;
                }

                base.Reset();
            }
        }
    }
}
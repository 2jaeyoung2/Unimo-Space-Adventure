using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Gathering (Spawned)")]

    public sealed class Gathering : SpawnedObject
    {
        [Line]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        private Collider mainCollider = null;

        public Collider MainCollider
        {
            get => mainCollider;
        }

        [Space]

        [Essential]

        [ReadOnlyWhenPlayMode]

        [UsingCustomProperty]

        [SerializeField]

        private GatheringData gatheringData = null;

        public GatheringData GatheringData
        {
            get => gatheringData;
        }

        private float currentHealth = 0f;

        public float CurrentHealth
        {
            get => currentHealth;
        }

        public override void Appear()
        {
            base.Appear();

            currentHealth = gatheringData.MaxHealth;
        }

        public override void OnAppeared()
        {
            base.OnAppeared();

            mainCollider.enabled = true;
        }

        public override void Disappear()
        {
            mainCollider.enabled = false;

            base.Disappear();
        }

        public override void OnDisappeared()
        {
            base.OnDisappeared();
        }

        public void Harvest(float damage, Transform harvester)
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;

                ++StageQuestList.Instance.GatheringCount;

                var harvestVFX = ObjectPoolManager.Instance.Clone<HarvestVFX>("Harvest VFX");

                harvestVFX.transform.position = transform.position;

                harvestVFX.Play(harvester);

                Disappear();
            }
        }
    }
}
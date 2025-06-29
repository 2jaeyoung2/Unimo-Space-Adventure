using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

using ZL.Unity.Debugging;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class Spawner : MonoBehaviour
    {
        [Line]

        [Text("<b>기본 옵션</b>", FontSize = 16)]

        [Margin]

        [Text("<b>스폰 할 오브젝트 이름</b>")]

        [UsingCustomProperty]

        [SerializeField]

        private string spawnObjectName = "";

        [Space]

        [Text("<b>웨이브 횟수 (0: 무한)</b>")]

        [UsingCustomProperty]

        [SerializeField]

        private int waveCount = 0;

        [Space]

        [Text("<b>첫 웨이브 간격 (-1: 지정 범위, 0: 즉시 첫 웨이브)</b>")]

        [UsingCustomProperty]

        [SerializeField]

        private float waveInterval = -1f;

        [Space]

        [Text("<b>웨이브 최소 간격</b>")]

        [UsingCustomProperty]

        [SerializeField]

        private float minWaveInterval = 0f;

        [Space]

        [Text("<b>웨이브 최대 간격</b>")]

        [UsingCustomProperty]

        [SerializeField]

        private float maxWaveInterval = 0f;

        [Space]

        [Text("<b>스폰 사이의 딜레이</b>")]

        [UsingCustomProperty]

        [SerializeField]

        protected float spawnDelay = 0f;

        [Space]

        [Text("<b>스폰된 오브젝트의 수명 (-1: 무한)</b>")]

        [UsingCustomProperty]

        [SerializeField]

        protected float lifeTime = -1f;

        [Space]

        [Text("<b>추적할 목표</b>")]

        [UsingCustomProperty]

        [SerializeField]

        protected Transform destination = null;

        [Space]

        [Text("<b>목표를 향해 회전하는 속도 배수</b>")]

        [UsingCustomProperty]

        [SerializeField]

        private float rotationSpeedMultiplier = 1f;

        public float RotationSpeedMultiplier
        {
            get => rotationSpeedMultiplier;
        }

        [Space]

        [Text("<b>목표를 향해 이동하는 속도 배수</b>")]

        [UsingCustomProperty]

        [SerializeField]

        private float movementSpeedMultiplier = 1f;

        public float MovementSpeedMultiplier
        {
            get => movementSpeedMultiplier;
        }

        [Space]

        [Text("<b>오브젝트가 디스폰되는 거리 (-1: 무한)</b>")]

        [UsingCustomProperty]

        [SerializeField]

        protected float despawnDistance = -1f;

        [Line]

        [Text("<b>스폰 시 바라볼 대상 (None: 지정 방향)</b>")]

        [UsingCustomProperty]

        [SerializeField]

        protected Transform lookPoint = null;

        protected int objectCount = 0;

        protected virtual void OnDrawGizmosSelected()
        {
            if (despawnDistance == -1f)
            {
                return;
            }

            Gizmos.color = new(1f, 0f, 0f, 0.5f);

            GizmosEx.DrawPolygon(transform.position, despawnDistance, 64);
        }

        private void OnEnable()
        {
            StartSpawning();
        }

        private void OnDisable()
        {
            spawningRoutine = null;
        }

        public void StartSpawning()
        {
            if (spawningRoutine != null)
            {
                return;
            }

            spawningRoutine = SpawningRoutine();

            StartCoroutine(spawningRoutine);
        }

        public void StopSpawning()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator spawningRoutine = null;

        private IEnumerator SpawningRoutine()
        {
            int waveCount = this.waveCount;

            float waveInterval = this.waveInterval;

            if (waveInterval == -1f)
            {
                waveInterval = Random.Range(minWaveInterval, maxWaveInterval);
            }

            while (true)
            {
                if (waveInterval != 0f)
                {
                    yield return WaitForSecondsCache.Get(waveInterval);
                }

                yield return WaveRoutine();

                if (this.waveCount != 0 && --waveCount <= 0)
                {
                    break;
                }

                waveInterval = Random.Range(minWaveInterval, maxWaveInterval);
            }

            StopSpawning();
        }

        protected abstract IEnumerator WaveRoutine();

        protected void Spawn(Vector3 position)
        {
            Spawn(position, Quaternion.identity);
        }

        protected void Spawn(Vector3 position, Quaternion rotation)
        {
            ++objectCount;

            var spawnedObject = ObjectPoolManager.Instance.Clone<SpawnedObject>(spawnObjectName);

            spawnedObject.transform.SetPositionAndRotation(position, rotation);

            spawnedObject.OnDisappearedAction += Despawn;

            spawnedObject.LifeTime = lifeTime;

            spawnedObject.Destination = destination;

            spawnedObject.RotationSpeedMultiplier = rotationSpeedMultiplier;

            spawnedObject.MovementSpeedMultiplier = movementSpeedMultiplier;

            spawnedObject.Appear();

            spawnedObject.DespawnDistance = despawnDistance;

            spawnedObject.SpawnPosition = transform.position;
        }

        private void Despawn()
        {
            --objectCount;
        }
    }
}
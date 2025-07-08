using UnityEngine;

using ZL.Unity.Debugging;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Circular Random Spawner")]

    public sealed class CircularRandomSpawner : RandomSpawner
    {
        [Space]

        [Text("<b>스폰 범위</b>")]

        [UsingCustomProperty]

        [SerializeField]

        private float spawnRange = 0f;

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            Gizmos.color = new(0f, 1f, 0f, 0.5f);

            GizmosEx.DrawPolygon(transform.position, spawnRange, 64);
        }

        protected override void Spawn()
        {
            var randomPoint = Random.insideUnitCircle * spawnRange;

            var spawnPosition = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);

            var spawnRotation = GetSpawnRotation(spawnPosition);

            Spawn(spawnPosition, spawnRotation);
        }
    }
}
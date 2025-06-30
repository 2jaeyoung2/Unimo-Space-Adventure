using UnityEngine;

using UnityEngine.Serialization;

using ZL.Unity.Pooling;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy UI Screen (Singleton)")]

    public sealed class EnemyUIScreen : ScreenUI<EnemyUIScreen>
    {
        [Space]

        [SerializeField]

        [FormerlySerializedAs("enemyHealthBarPool")]

        private DictionaryObjectPool<Enemy, NamedEnemyHealthBar> enemyHealthBarPool = null;

        public void AppearNamedEnemyHealthBar(Enemy targetEnemy)
        {
            if (enemyHealthBarPool.TryClone(targetEnemy, out var healthBar) == true)
            {
                healthBar.Initialize(targetEnemy);

                healthBar.Appear();
            }
        }

        public void DisappearNamedEnemyHealthBar(Enemy targetEnemy)
        {
            if (enemyHealthBarPool.ContainsKey(targetEnemy) == true)
            {
                enemyHealthBarPool[targetEnemy].Disappear();
            }
        }
    }
}
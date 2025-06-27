using UnityEngine;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy Manager (Singleton)")]

    public sealed class EnemyManager : MonoSingleton<EnemyManager>
    {
        [Space]

        [SerializeField]

        private Transform skillTarget = null;

        public Transform SkillTarget
        {
            get => skillTarget;
        }
    }
}
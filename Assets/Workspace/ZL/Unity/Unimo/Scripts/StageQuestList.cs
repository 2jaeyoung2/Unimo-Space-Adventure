using TMPro;

using UnityEngine;

using UnityEngine.Events;

using ZL.CS;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Quest List (Singleton")]

    public sealed class StageQuestList : MonoSingleton<StageQuestList>
    {
        [Space]

        [Essential]

        [ReadOnlyWhenPlayMode]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI gatherProgressTextUI = null;

        [Essential]

        [ReadOnlyWhenPlayMode]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI bossKillProgressTextUI = null;

        [Space]

        [SerializeField]

        private UnityEvent onQuestCompletedEvent = null;

        public UnityEvent OnQuestCompletedEvent
        {
            get => onQuestCompletedEvent;
        }

        private int targetGatheringCount = 0;

        private int gatheringCount = 0;

        public int GatheringCount
        {
            get => gatheringCount;

            set
            {
                gatheringCount = MathEx.Clamp(value, 0, targetGatheringCount);

                gatherProgressTextUI.text = $"목표 자원: {gatheringCount}/{targetGatheringCount}";

                CheckQuest();
            }
        }

        private int targetBossKillCount = 0;

        private int bossKillCount = 0;

        public int BossKillCount
        {
            get => bossKillCount;

            set
            {
                bossKillCount = MathEx.Clamp(value, 0, targetBossKillCount);

                bossKillProgressTextUI.text = $"보스 처치: {bossKillCount}/{targetBossKillCount}";

                CheckQuest();
            }
        }

        private void Start()
        {
            targetGatheringCount = StageQuestData.Instance.TargetGatheringCount;

            if (targetGatheringCount > 0)
            {
                GatheringCount = 0;

                gatherProgressTextUI.gameObject.SetActive(true);
            }

            targetBossKillCount = StageQuestData.Instance.TargetBossKillCount;

            if (targetBossKillCount > 0)
            {
                BossKillCount = 0;

                bossKillProgressTextUI.gameObject.SetActive(true);
            }
        }

        private void CheckQuest()
        {
            if (gatheringCount < targetGatheringCount)
            {
                return;
            }

            if (bossKillCount < targetBossKillCount)
            {
                return;
            }

            onQuestCompletedEvent.Invoke();

            onQuestCompletedEvent.RemoveAllListeners();
        }
    }
}
using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Stage Quest Data", fileName = "Stage Quest Data 1")]

    public sealed class StageQuestData : ScriptableGoogleSheetData, ISingleton<StageQuestData>
    {
        public static StageQuestData Instance
        {
            get => ISingleton<StageQuestData>.Instance;
        }

        [Space]

        [SerializeField]

        private int targetGatheringCount = 0;

        public int TargetGatheringCount
        {
            get => targetGatheringCount;
        }

        [SerializeField]

        private int targetBossKillCount = 0;

        public int TargetBossKillCount
        {
            get => targetBossKillCount;
        }

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(targetGatheringCount),

                nameof(targetBossKillCount),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            targetGatheringCount = int.Parse(sheet[name, nameof(targetGatheringCount)].value);

            targetBossKillCount = int.Parse(sheet[name, nameof(targetBossKillCount)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                targetGatheringCount.ToString(),

                targetBossKillCount.ToString(),
            };
        }
    }
}
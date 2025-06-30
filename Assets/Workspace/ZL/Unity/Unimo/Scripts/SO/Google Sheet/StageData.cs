using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Stage Data", fileName = "Stage Data 1")]

    public sealed class StageData : ScriptableGoogleSheetData, ISingleton<StageData>
    {
        public static StageData Instance
        {
            get => ISingleton<StageData>.Instance;
        }

        [Space]

        [SerializeField]

        private float fuelConsumptionAmount = 0f;

        public float FuelConsumptionAmount
        {
            get => fuelConsumptionAmount;
        }

        [Space]

        [SerializeField]

        private int inGameMoneyAmountMin = 0;

        public int InGameMoneyAmountMin
        {
            get => inGameMoneyAmountMin;
        }

        [SerializeField]

        private int inGameMoneyAmountMax = 0;

        public int InGameMoneyAmountMax
        {
            get => inGameMoneyAmountMax;
        }

        [Space]

        [SerializeField]

        private int outGameMoneyAmountMin = 0;

        public int OutGameMoneyAmountMin
        {
            get => outGameMoneyAmountMin;
        }

        [SerializeField]

        private int outGameMoneyAmountMax = 0;

        public int OutGameMoneyAmountMax
        {
            get => outGameMoneyAmountMax;
        }

        [Space]

        [SerializeField]

        private int bluePrintCount = 0;

        public int BluePrintCount
        {
            get => bluePrintCount;
        }

        [Space]

        [SerializeField]

        private float relicChance = 0f;

        public float RelicChance
        {
            get => relicChance;
        }

        [SerializeField]

        private int relicCount = 0;

        public int RelicCount
        {
            get => relicCount;
        }

        [Space]

        [SerializeField]

        private int score = 0;

        public int Score
        {
            get => score;
        }

        public static int DropedInGameMoneyAmount { get; private set; } = 0;

        public static int DropedOutGameMoneyAmount { get; private set; } = 0;

        public static int DropedBluePrintCount { get; private set; } = 0;

        public static RelicData[] DropedRelicDatas { get; private set; } = null;

        public static int TotalScore { get; private set;} = 0;

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(fuelConsumptionAmount),

                nameof(inGameMoneyAmountMin),

                nameof(inGameMoneyAmountMax),

                nameof(outGameMoneyAmountMin),

                nameof(outGameMoneyAmountMax),

                nameof(bluePrintCount),

                nameof(relicChance),

                nameof(relicCount),

                nameof(score),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            fuelConsumptionAmount = float.Parse(sheet[name, nameof(fuelConsumptionAmount)].value);

            inGameMoneyAmountMin = int.Parse(sheet[name, nameof(inGameMoneyAmountMin)].value);

            inGameMoneyAmountMax = int.Parse(sheet[name, nameof(inGameMoneyAmountMax)].value);

            outGameMoneyAmountMin = int.Parse(sheet[name, nameof(outGameMoneyAmountMin)].value);

            outGameMoneyAmountMax = int.Parse(sheet[name, nameof(outGameMoneyAmountMax)].value);

            bluePrintCount = int.Parse(sheet[name, nameof(bluePrintCount)].value);

            relicChance = float.Parse(sheet[name, nameof(relicChance)].value);

            relicCount = int.Parse(sheet[name, nameof(relicCount)].value);

            score = int.Parse(sheet[name, nameof(score)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                fuelConsumptionAmount.ToString(),

                inGameMoneyAmountMin.ToString(),

                inGameMoneyAmountMax.ToString(),

                outGameMoneyAmountMin.ToString(),

                outGameMoneyAmountMax.ToString(),

                bluePrintCount.ToString(),

                relicChance.ToString(),

                relicCount.ToString(),

                score.ToString(),
            };
        }

        public void DropRewards()
        {
            DropedInGameMoneyAmount = Random.Range(inGameMoneyAmountMin, inGameMoneyAmountMax);

            DropedOutGameMoneyAmount = Random.Range(outGameMoneyAmountMin, outGameMoneyAmountMax);

            DropedBluePrintCount = bluePrintCount;

            DropedRelicDatas = null;

            if (RandomEx.DrawLots(relicChance) == true)
            {
                DropRelics();
            }

            TotalScore = score;
        }

        public void DropRelics()
        {
            DropedRelicDatas = RelicDropTable.Instance.GetRandomRelics(relicCount);
        }

        void ISingleton<StageData>.Release()
        {
            DropedInGameMoneyAmount = 0;

            DropedOutGameMoneyAmount = 0;

            DropedBluePrintCount = 0;

            DropedRelicDatas = null;

            TotalScore = 0;
        }
    }
}
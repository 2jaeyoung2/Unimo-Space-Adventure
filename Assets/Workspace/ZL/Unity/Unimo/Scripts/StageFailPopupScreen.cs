using TMPro;

using UnityEngine;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Fail Popup Screen")]

    public sealed class StageFailPopupScreen : ScreenUI
    {
        [Space]

        [Essential]

        [Alias("Stage Play Time Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI stagePlayTimeTextUI = null;

        [Essential]

        [Alias("Score Amount Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI scoreAmountTextUI = null;

        public override void Appear()
        {
            stagePlayTimeTextUI.text = $"ÇÃ·¹ÀÌ ½Ã°£: {SceneClock.Instance.GetTimeStamp()}";

            if (StageData.TotalScore != 0)
            {
                scoreAmountTextUI.text = $"È¹µæ Á¡¼ö: {StageData.TotalScore}";

                scoreAmountTextUI.gameObject.SetActive(true);
            }

            else
            {
                scoreAmountTextUI.gameObject.SetActive(false);
            }

            base.Appear();
        }
    }
}
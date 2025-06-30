using TMPro;

using UnityEngine;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Clear Popup Screen")]

    public sealed class StageClearPopupScreen : ScreenUI
    {
        [Space]

        [Essential]

        [UsingCustomProperty]

        [SerializeField]

        private ForceLayoutRebuilder popupContent = null;

        [Essential]

        [Alias("Stage Play Time Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI stagePlayTimeTextUI = null;

        [Essential]

        [Alias("In-Game Money Amount Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI inGameMoneyAmountTextUI = null;

        [Essential]

        [Alias("Out-Game Money Amount Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI outGameMoneyAmountTextUI = null;

        [Essential]

        [Alias("Blue Print Count Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI bluePrintCountTextUI = null;

        [Essential]

        [Alias("Score Amount Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI scoreAmountTextUI = null;

        public override void Appear()
        {
            stagePlayTimeTextUI.text = $"«√∑π¿Ã Ω√∞£: {SceneClock.Instance.GetTimeStamp()}";

            if (StageData.DropedInGameMoneyAmount != 0)
            {
                inGameMoneyAmountTextUI.text = $"»πµÊ ¿Œ ∞‘¿” ¿Á»≠: {StageData.DropedInGameMoneyAmount}";

                inGameMoneyAmountTextUI.gameObject.SetActive(true);
            }

            else
            {
                inGameMoneyAmountTextUI.gameObject.SetActive(false);
            }

            if (StageData.DropedOutGameMoneyAmount != 0)
            {
                outGameMoneyAmountTextUI.text = $"»πµÊ æ∆øÙ ∞‘¿” ¿Á»≠: {StageData.DropedOutGameMoneyAmount}";

                outGameMoneyAmountTextUI.gameObject.SetActive(true);
            }

            else
            {
                outGameMoneyAmountTextUI.gameObject.SetActive(false);
            }

            if (StageData.DropedBluePrintCount != 0)
            {
                bluePrintCountTextUI.text = $"»πµÊ º≥∞Ëµµ: {StageData.DropedBluePrintCount}";

                bluePrintCountTextUI.gameObject.SetActive(true);
            }

            else
            {
                bluePrintCountTextUI.gameObject.SetActive(false);
            }

            if (StageData.TotalScore != 0)
            {
                scoreAmountTextUI.text = $"»πµÊ ¡°ºˆ: {StageData.TotalScore}";

                scoreAmountTextUI.gameObject.SetActive(true);
            }

            else
            {
                scoreAmountTextUI.gameObject.SetActive(false);
            }

            popupContent.ForceRebuildLayout();

            base.Appear();
        }
    }
}
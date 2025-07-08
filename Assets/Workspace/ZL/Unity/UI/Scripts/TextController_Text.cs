using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (Legacy Text)")]

    public sealed class TextController_Text : TextController
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [Alias("Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private Text textUI = null;

        public override string text
        {
            get => textUI.text;

            set => textUI.text = value;
        }
    }
}
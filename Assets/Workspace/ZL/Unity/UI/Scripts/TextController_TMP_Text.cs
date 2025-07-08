using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (TMP Text)")]

    public sealed class TextController_TMP_Text : TextController
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [Alias("Text (UI)")]

        [PropertyField]

        [UsingCustomProperty]

        [SerializeField]

        private TMP_Text textUI = null;

        public override string text
        {
            get => textUI.text;

            set => textUI.text = value;
        }
    }
}
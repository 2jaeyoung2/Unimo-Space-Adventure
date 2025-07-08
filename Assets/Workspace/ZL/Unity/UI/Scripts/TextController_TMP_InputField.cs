using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (TMP Input Field)")]

    public sealed class TextController_TMP_InputField : TextController
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [Alias("Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TMP_InputField textUI = null;

        public override string text
        {
            get => textUI.text;

            set => textUI.text = value;
        }
    }
}
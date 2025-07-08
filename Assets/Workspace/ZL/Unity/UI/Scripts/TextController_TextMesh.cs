using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Text Controller (Text Mesh)")]

    public sealed class TextController_TextMesh : TextController
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [Alias("Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMesh textUI = null;

        public override string text
        {
            get => textUI.text;

            set => textUI.text = value;
        }
    }
}
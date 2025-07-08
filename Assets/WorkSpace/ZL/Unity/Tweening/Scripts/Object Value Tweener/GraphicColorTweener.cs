using DG.Tweening.Plugins.Options;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Graphic Color Tweener")]

    public sealed class GraphicColorTweener : ObjectValueTweener<ColorTweener, Color, Color, ColorOptions>
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        private Graphic graphic = null;

        public override Color Value
        {
            get => graphic.color;

            set => graphic.color = value;
        }
    }
}
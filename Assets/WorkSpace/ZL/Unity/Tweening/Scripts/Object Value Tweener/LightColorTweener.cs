using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Light Color Tweener")]

    public sealed class LightColorTweener : ObjectValueTweener<ColorTweener, Color, Color, ColorOptions>
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        #pragma warning disable CS0108

        private Light light = null;

        #pragma warning restore CS0108

        public override Color Value
        {
            get => light.color;
            
            set => light.color = value;
        }
    }
}
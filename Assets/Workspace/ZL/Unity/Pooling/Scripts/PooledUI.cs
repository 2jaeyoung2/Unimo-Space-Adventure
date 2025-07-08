using UnityEngine;

using ZL.Unity.Tweening;

namespace ZL.Unity.Pooling
{
    public abstract class PooledUI : PooledObject
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [PropertyField]

        [Margin]

        [ReadOnlyWhenEditMode]

        [Button(nameof(Appear))]

        [Button(nameof(Disappear))]

        [UsingCustomProperty]

        [SerializeField]

        protected Fader fader = null;

        private void Awake()
        {
            fader.OnFadedOutEvent.AddListener(OnDisappeared);
        }

        public override void Appear()
        {
            fader.FadeIn();
        }

        public override void Disappear()
        {
            fader.FadeOut();
        }
    }
}
using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Rect Transform Fitter")]

    public sealed class RectTransformFitter : MonoBehaviour
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        private RectTransform rectTransform = null;

        [Space]

        [Essential]

        [Button(nameof(FitParentSizeMin))]

        [Button(nameof(FitParentSizeMax))]

        [ReadOnlyWhenPlayMode]

        [UsingCustomProperty]

        [SerializeField]

        private RectTransform fitTarget = null;

        public void FitParentSizeMin()
        {
            rectTransform.FitSizeMin(fitTarget.sizeDelta);
        }

        public void FitParentSizeMax()
        {
            rectTransform.FitSizeMax(fitTarget.sizeDelta);
        }
    }
}
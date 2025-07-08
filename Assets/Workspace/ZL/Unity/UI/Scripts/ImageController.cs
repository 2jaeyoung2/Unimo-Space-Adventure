using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Image Controller")]

    public sealed class ImageController : MonoBehaviour
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        private Image image = null;

        [Space]

        [Range(0f, 1f)]

        [SerializeField]

        private float alphaHitTestMinimumThreshold = 0f;

        private void Awake()
        {
            image.alphaHitTestMinimumThreshold = alphaHitTestMinimumThreshold;
        }

        private void OnValidate()
        {
            if (Application.isPlaying == true)
            {
                image.alphaHitTestMinimumThreshold = alphaHitTestMinimumThreshold;
            }
        }

        #if UNITY_EDITOR

        private void Update()
        {
            alphaHitTestMinimumThreshold = image.alphaHitTestMinimumThreshold;
        }

        #endif
    }
}
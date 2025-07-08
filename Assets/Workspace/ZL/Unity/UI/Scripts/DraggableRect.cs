using UnityEngine;

using UnityEngine.EventSystems;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Draggable Rect")]

    public sealed class DraggableRect : MonoBehaviour, IDragHandler
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        private RectTransform rectTransform;

        [Space]

        [GetComponentInParent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        private Canvas canvas;

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}
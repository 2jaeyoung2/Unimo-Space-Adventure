using UnityEngine;

using UnityEngine.UI;

using Axis = UnityEngine.UI.GridLayoutGroup.Axis;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Grid Layout Group Cell Size Fitter")]

    public sealed class GridLayoutGroupCellSizeFitter : MonoBehaviour
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        private GridLayoutGroup gridLayoutGroup = null;

        [GetComponentInParentOnly]

        [Essential]

        [Button(nameof(Fit))]

        [UsingCustomProperty]

        [SerializeField]

        private RectTransform container = null;

        public void Fit()
        {
            var cellSize = gridLayoutGroup.cellSize;

            if (gridLayoutGroup.startAxis == Axis.Horizontal)
            {
                cellSize.y = container.rect.height - gridLayoutGroup.padding.vertical;
            }

            if (gridLayoutGroup.startAxis == Axis.Vertical)
            {
                cellSize.x = container.rect.width - gridLayoutGroup.padding.horizontal;
            }

            if (gridLayoutGroup.cellSize != cellSize)
            {
                FixedUndo.RecordObject(gridLayoutGroup, "Fit Grid Layout Group Cell Size");

                gridLayoutGroup.cellSize = cellSize;

                FixedEditorUtility.SetDirty(gridLayoutGroup);
            }
        }
    }
}
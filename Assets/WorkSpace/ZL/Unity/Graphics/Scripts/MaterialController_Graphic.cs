using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.GFX
{
    [AddComponentMenu("ZL/GFX/Material Controller (Graphic)")]

    public sealed class MaterialController_Graphic : MaterialController
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnlyWhenPlayMode]

        [UsingCustomProperty]

        [SerializeField]

        private Graphic targetGraphic = null;

        [Space]

        [SerializeField]

        private bool isShared = false;

        public override Material Material
        {
            get => targetGraphic.material;
        }

        private void Awake()
        {
            if (isShared == false)
            {
                targetGraphic.material = new Material(targetGraphic.material);
            }
        }
    }
}
using UnityEngine;

namespace ZL.Unity.GFX
{
    [AddComponentMenu("ZL/GFX/Material Controller (Renderer)")]

    public sealed class MaterialController_Renderer : MaterialController
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnlyWhenPlayMode]

        [UsingCustomProperty]

        [SerializeField]

        private Renderer targetRenderer = null;

        [Space]

        [SerializeField]

        private int index = 0;

        public override Material Material
        {
            get => Materials[index];
        }

        public Material[] Materials
        {
            get => targetRenderer.materials;
        }
    }
}
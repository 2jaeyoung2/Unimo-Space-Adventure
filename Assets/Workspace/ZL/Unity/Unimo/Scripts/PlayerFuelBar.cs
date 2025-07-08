using UnityEngine;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player Fuel Bar")]

    public sealed class PlayerFuelBar : MonoBehaviour
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        private SliderValueDisplayer sliderValueDisplayer = null;

        private void Start()
        {
            sliderValueDisplayer.SetMaxValue(PlayerFuelManager.MaxFuel);

            sliderValueDisplayer.SetValue(PlayerFuelManager.Fuel);

            PlayerFuelManager.Instance.OnMaxFuelChangedAction += sliderValueDisplayer.SetMaxValue;

            PlayerFuelManager.Instance.OnFuelChangedAction += sliderValueDisplayer.SetValue;
        }
    }
}
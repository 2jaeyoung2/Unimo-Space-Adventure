using System;

using TMPro;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.SO;

using ZL.Unity.SO.GoogleSheet;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Relic Card (Pooled)")]

    public sealed class RelicCard : PooledObject
    {
        [Space]

        [GetComponent]

        [Essential]

        [ReadOnlyWhenPlayMode]

        [UsingCustomProperty]

        [SerializeField]

        private Toggle toggle = null;

        public Toggle Toggle
        {
            get => toggle;
        }

        [Space]

        [Essential]

        [Alias("Rarity Hightlight Image (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private Image rarityHightlightImageUI = null;

        [Essential]

        [Alias("Relic Image (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private Image relicImageUI = null;

        [Essential]

        [Alias("Relic Name Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI relicNameTextUI = null;

        [Essential]

        [Alias("Relic Description Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextMeshProUGUI relicDescriptionTextUI = null;

        private StringTable relicNameStringTable = null;

        private StringTable relicDescriptionStringTable = null;

        [Space]

        [Essential]

        [UsingCustomProperty]

        [SerializeField]

        private ImageTable relicImageTable = null;

        [Essential]

        [UsingCustomProperty]

        [SerializeField]

        private StringTableSheet relicStringTableSheet = null;

        [Space]

        [SerializeField]

        private RelicData relicData = null;

        public RelicData RelicData
        {
            get => relicData;
        }

        public event Action<RelicCard> OnSelectAction = null;

        public event Action<RelicCard> OnDeselectAction = null;

        public void Initialize(RelicData relicData)
        {
            this.relicData = relicData;

            rarityHightlightImageUI.color = relicData.Rarity.GetColor();

            relicImageUI.sprite = relicImageTable[relicData.name];

            relicNameStringTable = relicStringTableSheet[relicData.name + " Name"];

            relicDescriptionStringTable = relicStringTableSheet[relicData.name + " Description"];
        }

        public override void Appear()
        {
            base.Appear();

            RefreshTexts();

            StringTableManager.Instance.OnLanguageChangedAction += RefreshTexts;
        }

        public override void Disappear()
        {
            toggle.isOn = false;

            OnSelectAction = null;

            OnDeselectAction = null;

            StringTableManager.Instance.OnLanguageChangedAction -= RefreshTexts;

            base.Disappear();
        }

        private void RefreshTexts()
        {
            relicNameTextUI.text = relicNameStringTable.Value;

            relicData.Refresh();

            relicDescriptionTextUI.text = string.Format(relicDescriptionStringTable.Value, relicData.EffectsArgs);
        }

        public void SetSelect(bool value)
        {
            if (value == true)
            {
                OnSelectAction?.Invoke(this);
            }

            else
            {
                OnDeselectAction?.Invoke(this);
            }
        }
    }
}
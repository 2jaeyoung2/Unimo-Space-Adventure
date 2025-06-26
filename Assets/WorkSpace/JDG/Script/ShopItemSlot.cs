using UnityEngine;

using TMPro;

using UnityEngine.UI;

using ZL.Unity.Unimo;

namespace JDG
{
    public class ShopItemSlot : MonoBehaviour
    {
        [SerializeField]
        
        private TextMeshProUGUI _relicName;

        [SerializeField]
        
        private Image _relicIcon;

        [SerializeField]

        private Image _resourceIcon;

        [SerializeField]

        private TextMeshProUGUI _relicPrice;

        [SerializeField]

        private Button _buyButton;

        [SerializeField]

        private GameObject _disabledOverlay;

        [SerializeField]

        private ImageTable _imageTable;

        [SerializeField]

        private Sprite _resourceSpite;

        private RelicData _relicData;

        //private bool _isBuy;

        public void SetShopItemSlot(RelicData data)
        {
            _relicData = data;

            _relicName.text = data.name;

            if (_imageTable != null && _imageTable[data.name] != null)
            {
                _relicIcon.sprite = _imageTable[data.name];
            }

            if (_resourceSpite != null)
            {
                _resourceIcon.sprite = _resourceSpite;
            }

            _relicPrice.text = data.Price.ToString();

            //_isBuy = false;

            _disabledOverlay.SetActive(false);

            _buyButton.interactable = true;
        }

        public void OnBuyButtonClicked()
        {
            if (UIManager.Instance.IsResultUIOpen)
                return;

            //아이템 가격
            int relicPrice = _relicData.Price;

            //플레이어 소지금 감소
            if (ConditionChecker.IsEnoughPlayerResource(relicPrice, ResourcesType.IngameCurrency))
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-relicPrice);

                PlayerInventoryManager.AddRelic(_relicData);

                _disabledOverlay.SetActive(true);

                _buyButton.interactable = false;

                PlayerEvents.ChangeCurrency();

                PlayerEvents.ChangeRelic();

                UIManager.Instance.ShopUI.ShowResultPanel($"{_relicName}을 구입하였습니다");
            }

            else
            {
                UIManager.Instance.ShopUI.ShowResultPanel("소지금이 부족합니다");
            }
        }
    }
}
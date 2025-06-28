using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZL.Unity.Unimo;

namespace JDG
{
    public class ShopUI : MonoBehaviour
    {
        [Header("UI창 생성할때 필요한 것들")]
        [SerializeField] private GameObject _root;
        [SerializeField] private Transform _slotParent;
        [SerializeField] private TextMeshProUGUI _resourceText1;
        [SerializeField] private TextMeshProUGUI _resourceText2;
        [SerializeField] private Sprite _resourceSpite;
        [SerializeField] private Image _resourceIcon1;
        [SerializeField] private Image _resourceIcon2;
        //혹시 필요할까봐 만들어둠 필요없으면 지울것
        //[SerializeField] private Image _repairIcon;
        //[SerializeField] private TextMeshProUGUI _repairButtonText1;
        //[SerializeField] private TextMeshProUGUI _repairButtonText2;
        private GameObject _slotPrefab;

        [Header("UI창 위치 조정")]
        [SerializeField] private Vector3 _offset;

        [Header("아이템 선택지 갯수 조정")]
        [SerializeField] private int _itemCount;

        [Header("수리비 조정")]
        [SerializeField] private int _repair1Price;
        [SerializeField] private int _repair2Price;

        [Header("결과 보여주는 패널")]
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private TextMeshProUGUI _resultText;

        private void Start()
        {
            HideShopUI();
            HideResultPanel();
        }

        public int ItemCount => _itemCount;

        public void OpenShopUI(List<RelicData> relics, Vector3 worldPos)
        {
            _root.SetActive(true);
            transform.position = worldPos + _offset;
            _slotPrefab = Resources.Load<GameObject>("WorldMap/RelicSlot");
            _resourceText1.text = _repair1Price.ToString();
            _resourceIcon1.sprite = _resourceSpite;
            _resourceText2.text = _repair2Price.ToString();
            _resourceIcon2.sprite = _resourceSpite;
            foreach (Transform child in _slotParent)
            {
                Destroy(child.gameObject);
            }

            foreach(var relic in relics)
            {
                GameObject obj = Instantiate(_slotPrefab, _slotParent);
                ShopItemSlot shopSlot = obj.GetComponent<ShopItemSlot>();
                shopSlot.SetShopItemSlot(relic);
            }
        }

        public void HideShopUI()
        {
            if (UIManager.Instance.IsResultUIOpen)
                return;

            _root.SetActive(false);
            UIManager.Instance.IsUIOpen = false;
        }

        public void ShowResultPanel(string message)
        {
            _resultText.text = message;
            _resultPanel.SetActive(true);
            UIManager.Instance.IsResultUIOpen = true;
        }

        public void HideResultPanel()
        {
            _resultPanel.SetActive(false);
            UIManager.Instance.IsResultUIOpen = false;
        }

        public void On10RepairButtonClicked()
        {
            if (UIManager.Instance.IsResultUIOpen)
                return;

            //소지금 감소
            if(ConditionChecker.IsEnoughPlayerResource(_repair1Price, ResourcesType.IngameCurrency))
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-_repair1Price);

                float maxHP = PlayerManager.PlayerStatus.maxHealth;

                float tenPer = maxHP / 10;

                float currentHP = PlayerManager.PlayerStatus.currentHealth;
                currentHP += tenPer;
                if(currentHP >= maxHP)
                {
                    currentHP = maxHP;
                }

                PlayerEvents.ChangeCurrency();

                ShowResultPanel("10%의 체력이 회복되었습니다");
            }
            else
            {
                ShowResultPanel("소지금이 부족합니다");
            }
        }

        public void On100RepairButtonClicked()
        {
            if (UIManager.Instance.IsResultUIOpen)
                return;

            if (ConditionChecker.IsEnoughPlayerResource(_repair2Price, ResourcesType.IngameCurrency))
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(-_repair2Price);

                float maxHP = PlayerManager.PlayerStatus.maxHealth ;

                float currentHP = PlayerManager.PlayerStatus.currentHealth;
                currentHP += maxHP;
                if(currentHP >= maxHP)
                {
                    currentHP = maxHP;
                }

                PlayerEvents.ChangeCurrency();

                ShowResultPanel("100%의 체력이 회복되었습니다");
            }
            else
            {
                ShowResultPanel("소지금이 부족합니다");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using ZL.Unity;
using ZL.Unity.Unimo;

public class WorldMapPlayerInvenUI : MonoBehaviour
{
    [Header("UI ฐüทร")]
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Transform _slotParent;
    [SerializeField] private int _slotsPerPage;

    private int _ingame;
    private int _meta;
    private int _blueprint;
    private int _currentPage = 0;
    private List<RelicData> _relicDatas;

    private void OnEnable()
    {
        PlayerEvents._OnRelicChanged += UpdateRelicUI;
        StartCoroutine(DelayedInit());
        _currentPage = 0;
    }

    private void OnDisable()
    {
        PlayerEvents._OnRelicChanged -= UpdateRelicUI;
    }

    public void UpdateRelicUI()
    {
        _relicDatas = new List<RelicData>(PlayerInventoryManager.RelicDatas);
        int start = _currentPage * _slotsPerPage;
        int end = Mathf.Min(start + _slotsPerPage, _relicDatas.Count);

        var slotPrefab = Resources.Load<GameObject>("WorldMap/RelicInvenSlotPanel");

        foreach (Transform child in _slotParent)
        {
            Destroy(child.gameObject);
        }

        for(int i = start; i < end; i++)
        {
            var relic = _relicDatas[i];
            var obj = Instantiate(slotPrefab, _slotParent);
            var slot = obj.GetComponent<RelicUISlot>();
            slot.Init(relic);
        }

        _prevButton.interactable = _currentPage > 0;

        _nextButton.interactable = (_currentPage + 1) * _slotsPerPage < _relicDatas.Count;
    }

    private void PrevPage()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
            UpdateRelicUI();
        }
    }

    private void NextPage()
    {
        if ((_currentPage + 1) * _slotsPerPage < _relicDatas.Count)
        {
            _currentPage++;
            UpdateRelicUI();
        }
    }

    private IEnumerator DelayedInit()
    {
        yield return null;

        UpdateRelicUI();
    }
}

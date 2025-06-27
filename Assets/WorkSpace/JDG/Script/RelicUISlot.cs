using JDG;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZL.Unity.Pooling;
using ZL.Unity.Unimo;

public class RelicUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _relicImage;
    [SerializeField] private TextMeshProUGUI _relicName;
    [SerializeField] private ImageTable _imageTable;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private Vector3 _offset;
    private RelicData _data;

    public void Init(RelicData data)
    {
        _data = data;
        _relicImage.sprite = _imageTable[data.name];
        _relicName.text = data.name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_data != null)
        {
            var obj = Instantiate(_cardPrefab, _parent);
            obj.transform.position = transform.position + _offset;
            var card = obj.GetComponent<RelicCard>();
            card.Initialize(_data);
            card.Appear();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
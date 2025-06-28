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
    [SerializeField] private Transform _parent;
    [SerializeField] private Vector3 _offset;
    private RelicData _data;
    private RelicCard _activeCard;

    public void Init(RelicData data)
    {
        _data = data;
        _relicImage.sprite = _imageTable[data.name];
        _relicName.text = data.name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_activeCard != null)
        {
            RelicCardPool.Instance.Release(_activeCard);
            _activeCard = null;
        }

        if (_data != null)
        {
            Vector3 finalOffset = _offset;
            Vector3 cardWorldPos = transform.position + transform.TransformVector(finalOffset);
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, cardWorldPos);

            if(screenPos.x > Screen.width * 0.9f)
            {
                finalOffset *= -1f;
            }

            Transform container = GameObject.Find("InvenRelicCardContainer").transform;

            _activeCard = RelicCardPool.Instance.Get(_parent, finalOffset);
            _activeCard.transform.SetParent(container, worldPositionStays: true);
            _activeCard.Initialize(_data);
            _activeCard.Appear();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_activeCard != null)
        {
            RelicCardPool.Instance.Release(_activeCard);
            _activeCard = null;
        }
    }
}
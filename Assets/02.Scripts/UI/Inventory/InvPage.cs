using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvPage : MonoBehaviour
{
    public ItemDataSO ItemData;
    public bool IsHaveItem { get; private set; } = false;
    [SerializeField] private int num = 1;
    [SerializeField] private RectTransform _childTransform;
    [SerializeField] private TextMeshProUGUI _itemText;
    [SerializeField] private TextMeshProUGUI _itemNumText;
    [SerializeField] private Image _itemImage;


    private Vector2 _originPos;

    private void Awake()
    {
        _originPos = _childTransform.anchoredPosition;
    }

    private void Start()
    {
        _itemText.gameObject.SetActive(false);
        _itemImage.gameObject.SetActive(false);
        _itemNumText.text = num.ToString();
    }
    private void Update()
    {
        if(ItemData != null)
            IsHaveItem = true;
        else
            IsHaveItem = false;
    }
    public void PutItem()
    {
        IsHaveItem = true;
    }

    public void MoveInv(bool isMove)
    {
        _childTransform.DOKill();

        if (isMove)
            _childTransform.DOAnchorPosX(_originPos.x - 30, 0.2f);
        else
            _childTransform.DOAnchorPosX(_originPos.x, 0.2f);
    }

    public void InvUISet(bool isSelect)
    {
        _itemText.gameObject.SetActive(isSelect);
        if(ItemData != null)
        {
            _itemImage.gameObject.SetActive(true);
            _itemText.text = ItemData.ItemName;
            _itemImage.sprite = ItemData.ItemImg;
        }
        else
            _itemImage.gameObject.SetActive(false);
    }
}
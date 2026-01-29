using TMPro;
using UnityEngine;

public class PickUpUIManager : MonoSingleton<PickUpUIManager>
{
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemHowPickText;
    [SerializeField] private GameObject _eKeyImage;

    public bool IsCanUse { get; private set; }

    public void CanUseEKey(bool isCan)
    {
        IsCanUse = isCan;
        _itemNameText.gameObject.SetActive(isCan);
        _itemHowPickText.gameObject.SetActive(isCan);
        _eKeyImage.SetActive(isCan);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/Item/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    public string ItemName;
    public Sprite ItemImg;

    public float HeavyAmount;
    public float HealAmount;
    public float EnergyAmount;

    public bool CanUse;
}
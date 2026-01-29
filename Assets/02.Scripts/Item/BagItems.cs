using UnityEngine;

public interface IItemAble
{
    bool _isTrigger { get; set; }
    void PlayerItemTriggerIn();
    void PlayerItemTriggerOut();
}

public class BagItems : MonoBehaviour, IItemAble
{
    public bool _isTrigger { get; set; }

    public void PlayerItemTriggerIn()
    {
        PickUpUIManager.Instance.CanUseEKey(true);
    }

    public void PlayerItemTriggerOut()
    {
        PickUpUIManager.Instance.CanUseEKey(false);
    }
}

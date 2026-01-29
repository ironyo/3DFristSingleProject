using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IItemAble>(out IItemAble item))
        {
            item.PlayerItemTriggerIn();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IItemAble>(out IItemAble item))
        {
            item.PlayerItemTriggerOut();
        }
    }
}

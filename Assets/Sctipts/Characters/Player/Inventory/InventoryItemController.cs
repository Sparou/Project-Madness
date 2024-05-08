using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public ItemInventory item;

    public Button RemoveButton;

    public void RemoveItem()
    {
        Inventory.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void Set(ItemInventory newItem)
    {
        item = newItem;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public ItemInventory item;

    public void DropItem()
    {
        Inventory.Instance.Drop(item);
        Destroy(gameObject);
    }

    public void RemoveItem()
    {
        Inventory.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void Set(ItemInventory newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        item.UseItem();
        RemoveItem();
    }
}

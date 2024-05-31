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
        if (item.UseItem())
        {
            RemoveItem();
        }
    }

    public void MoveToInventory()
    {
        Inventory.Instance.MoveToInventoryFromChest(item);
    }

    public void MoveToChest()
    {
        Inventory.Instance.MoveToChest(item);
        Debug.Log("MoveToChest");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] Transform Player;
    public ItemInventory Item;
    public int count = 1;

    public float TriggerDistance = 1;

    void Pickup()
    {
        if (Inventory.Instance.Add(Item, Inventory.Instance.InventoryItem)) {
            Destroy(gameObject);
        }
    }

    private bool CheckDistance()
    {
        var distance = Vector3.Distance(transform.position, Player.position);
        return distance < TriggerDistance;
    }

    void Update()
    {
        if (CheckDistance())
        {
            Hints.Instance.TurnOnWarning($"Поднять {Item.name} (Е)", 1f);

            if (Input.GetKeyUp(KeyCode.E))
            {
                Pickup();
            }
        }
    }
}

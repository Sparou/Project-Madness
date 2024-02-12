using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{

    private GameObject InventoryRef;

    public bool InventoryOpened = false;

    [SerializeField]  public int InventorySize = 10;

    [SerializeField]  private List<ItemInventory> inventory = new List<ItemInventory>();

    public void AddItemInventory(ItemInventory item)
    {
        if (inventory.Count < InventorySize)
        {
            inventory.Add(item);
            Debug.Log($"Предмет {item.Name} добавлен в инвентарь.");
        }
        else
        {
            Debug.Log("Инвентарь полон. Нельзя добавить больше предметов.");
        }
    }

    // Метод для удаления предмета из инвентаря
    public void RemoveItem(ItemInventory item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            Debug.Log($"Предмет {item.Name} удален из инвентаря.");
        }
        else
        {
            Debug.Log($"Предмет {item.Name} не найден в инвентаре.");
        }
    }

    // Метод для отображения содержимого инвентаря
    public void GetInventoryItems()
    {
        Debug.Log("Содержимое инвентаря:");
        foreach (var ItemInventory in inventory)
        {
            Debug.Log(ItemInventory.Name);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InventoryRef = GameObject.Find("Inventory");
        InventoryRef.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (!InventoryOpened)
            {
                OpenedInventory();
            }
            else
            {
                ClosedInventory();
            }
        }
    }

    public void OpenedInventory ()
    {
        InventoryRef.SetActive(true);
        InventoryOpened = true;
    }

    public void ClosedInventory()
    {
        InventoryRef.SetActive(false);
        InventoryOpened = false;
    }
}

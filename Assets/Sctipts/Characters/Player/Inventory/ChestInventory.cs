using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChestInventory : MonoBehaviour
{
    [SerializeField] private ChestAnimated chest;
    [SerializeField] Transform Player;

    public float TriggerDistance = 2;
    public Transform ChestItemContent;
    public GameObject ChestInventoryItem;


    [SerializeField] private List<ItemInventory> Items = new List<ItemInventory>();

    private GameObject InventoryRef;
    private bool InventoryOpened = false;


    public void ListItems()
    {
        CleanItemsContent();

        // Update items list
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(ChestInventoryItem, ChestItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemRef = obj.GetComponent<InventoryItemController>();

            itemName.text = item.Name;
            itemIcon.sprite = item.icon;
            itemRef.item = item;
        }
    }

    void Start()
    {
        InventoryRef = GameObject.Find("ChestInventory");
        InventoryRef.SetActive(false);
    }

    private bool CheckDistance()
    {
        var distance = Vector3.Distance(transform.position, Player.position);
        return distance < TriggerDistance;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && CheckDistance())
        {
            if (!InventoryOpened)
            {
                OpenedChest();
                chest.OpenChest();
            }
            else
            {
                ClosedChest();
                chest.CloseChest();
            }
        }
    }

    public void OpenedChest()
    {
        InventoryRef.SetActive(true);
        InventoryOpened = true;
        ListItems();

        Inventory.Instance.OpenedInventory();
    }

    public void ClosedChest()
    {
        InventoryRef.SetActive(false);
        InventoryOpened = false;
        CleanItemsContent();
        Inventory.Instance.ClosedInventory();
    }

    public void CleanItemsContent()
    {
        foreach (Transform item in ChestItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public void Add(ItemInventory item)
    {
        Items.Add(item);
        ListItems();
    }

    public void Remove(ItemInventory item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            ListItems();
        }
    }

    public void Get()
    {
        foreach (var ItemInventory in Items)
        {
            Debug.Log(ItemInventory.Name);

        }
    }
}



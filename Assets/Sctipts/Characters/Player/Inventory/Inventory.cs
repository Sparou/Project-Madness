using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private GameObject InventoryRef;

    public Player player;
    public bool InventoryOpened = false;

    [SerializeField]  public int InventorySize = 10;

    [SerializeField]  private List<ItemInventory> Items = new List<ItemInventory>();

    public static Inventory Instance;
    public Transform ItemContent;
    public GameObject InventoryItem;
    // public InventoryItemController[] InventoryItems;

    public GameObject ItemPref;
    public Transform LocationPlayer;
    public HealthController HealthPlayer;


    private void Awake()
    {
        Instance = this;
    }

    public void ListItems()
    {
        CleanItemsContent(); 

        // Update items list
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
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
        InventoryRef = GameObject.Find("Inventory");
        InventoryRef.SetActive(false);
        ListItems();
    }

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

    public void CleanItemsContent()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public void Add(ItemInventory item)
    {
        if (Items.Count < InventorySize)
        {
            Items.Add(item);
            ListItems();
            // Debug.Log($"Предмет {item.Name} добавлен в инвентарь.");
        }
        else
        {
            Debug.Log("Инвентарь полон. Нельзя добавить больше предметов.");
        }
    }

    public void Drop(ItemInventory item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            ListItems();

            var DropObj = Instantiate(ItemPref);
            DropObj.transform.position = LocationPlayer.position;

            var SpriteRender = DropObj.GetComponent<SpriteRenderer>();


            SpriteRender.sprite = item.icon;
            DropObj.GetComponent<ItemController>().Item = item;

            Debug.Log($"Предмет выброшен из инвентаря.");
        } else
        {
            Debug.Log($"Предмет не выброшен из инвентаря.");
        }
    }

    public void Remove(ItemInventory item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            ListItems();
            Debug.Log($"Предмет удален");
        }
        else
        {
            Debug.Log($"Предмет не удален");
        }
    }

    public void Get()
    {
        foreach (var ItemInventory in Items)
        {
            Debug.Log(ItemInventory.Name);

        }
    }

    //public void SetInventoryItems()
    //{
    //    InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
    //    for (int i = 0; i < Items.Count; i++)
    //    {
    //        InventoryItems[i].Set(Items[i]);
    //    } 
    //}
}
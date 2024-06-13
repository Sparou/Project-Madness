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
    public bool ChestOpened = false;

    [SerializeField]  public int InventorySize = 10;
    [SerializeField] public int CountCoins = 10;

    [SerializeField]  private List<ItemInventory> Items = new List<ItemInventory>();

    public static Inventory Instance;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject ChestItem;
    // public InventoryItemController[] InventoryItems;

    public GameObject ItemPref;
    public Transform LocationPlayer;
    public HealthController HealthPlayer;
    public TMP_Text CointsState;

    public ChestInventory Chest;


    private void Awake()
    {
        Instance = this;
    }

    public void ListItems(GameObject FromItem)
    {
        CleanItemsContent(); 

        // Update items list
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(FromItem, ItemContent);
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
        ListItems(InventoryItem);
        UpdateCoins();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I) && !ChestOpened)
        {
            if (!InventoryOpened)
            {
                OpenedInventory();
                InventoryOpened = true;
            }
            else
            {
                ClosedInventory();
                InventoryOpened = false;
            }
        }
    }

    public void OpenedInventory ()
    {
        InventoryRef.SetActive(true);
    }

    public void OpenedInventoryChest(ChestInventory InstChest)
    {
        InventoryRef.SetActive(true);
        ListItems(ChestItem);
        Chest = InstChest;
    }

    public void ClosedInventory()
    {
        InventoryRef.SetActive(false);
    }

    public void ClosedInventoryChest()
    {
        InventoryRef.SetActive(false);
        ListItems(InventoryItem);
    }

    public void CleanItemsContent()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public bool Add(ItemInventory item, GameObject FromItem)
    {
        if (Items.Count < InventorySize)
        {
            Items.Add(item);
            ListItems(FromItem);
            return true;
        }
        else
        {
            Hints.Instance.TurnOnWarning("Инвентарь полон. Нельзя добавить больше предметов.", 1f);
            return false;
        }
    }

    public void Drop(ItemInventory item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            ListItems(InventoryItem);

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

    public bool Remove(ItemInventory item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            ListItems(InventoryItem);
            Debug.Log($"Предмет удален");
            return true;
        }
        else
        {
            Debug.Log($"Предмет не удален");
            return false;
        }
    }

    public bool RemoveForChest(ItemInventory item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            ListItems(ChestItem);
            Debug.Log($"Предмет удален");
            return true;
        }
        else
        {
            Debug.Log($"Предмет не удален");
            return false;
        }
    }

    public void MoveToChest(ItemInventory item)
    {
        if (RemoveForChest(item))
        {
            Chest.Add(item);
        }
    }

    public void MoveToInventoryFromChest(ItemInventory item)
    {
        if (Add(item, ChestItem))
        {
            Chest.Remove(item);
        }
    }

    public void Get()
    {
        foreach (var ItemInventory in Items)
        {
            Debug.Log(ItemInventory.Name);

        }
    }

    public void GetCoins(int Coins)
    {
        CountCoins += Coins;
        UpdateCoins();
    }

    private void UpdateCoins()
    {
        CointsState.text = $"{CountCoins}";
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
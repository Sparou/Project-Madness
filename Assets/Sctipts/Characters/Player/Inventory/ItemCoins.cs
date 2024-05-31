using UnityEngine;


[CreateAssetMenu (fileName = "New Item", menuName = "Coins")]

public class ItemCoins : ItemInventory
{

    [SerializeField] public int CountCoins = 10;

    public override bool UseItem()
    {
        Inventory.Instance.GetCoins(CountCoins);
        return true;
    }

}

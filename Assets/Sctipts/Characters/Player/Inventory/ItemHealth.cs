using UnityEngine;


[CreateAssetMenu (fileName = "New Item", menuName = "Health")]

public class ItemHealth : ItemInventory
{

    [SerializeField] public int HealthRecovery = 100;

    public override bool UseItem()
    {
        if (Inventory.Instance.HealthPlayer.TakeHeal(HealthRecovery))
            return true;
        return false;
    }
}

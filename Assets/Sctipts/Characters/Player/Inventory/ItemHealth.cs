using UnityEngine;


[CreateAssetMenu (fileName = "New Item", menuName = "Health")]

public class ItemHealth : ItemInventory
{

    [SerializeField] public int HealthRecovery = 100;

    public override void UseItem()
    {
        Inventory.Instance.HealthPlayer.TakeHeal(HealthRecovery);
        Debug.Log(HealthRecovery);
    }
}

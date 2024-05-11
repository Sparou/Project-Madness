using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Weapon")]

public class ItemWeapon : ItemInventory
{

    [SerializeField] public int Damage;

    public override void UseItem()
    {
        return;
    }
}

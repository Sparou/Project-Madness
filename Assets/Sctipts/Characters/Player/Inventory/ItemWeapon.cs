using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Weapon")]

public class ItemWeapon : ItemInventory
{

    [SerializeField] public int Damage = 0;

    public override bool UseItem()
    {
        return false;
    }
}

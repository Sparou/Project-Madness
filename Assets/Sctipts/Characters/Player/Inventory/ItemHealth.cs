using UnityEngine;


[CreateAssetMenu (fileName = "New Item", menuName = "Health")]

public class ItemHealth : ItemInventory
{

    [SerializeField] public int HealthRecovery;
}

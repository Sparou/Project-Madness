using UnityEngine;


[CreateAssetMenu(fileName="New Item", menuName ="item/Create New Item")]
    public abstract class ItemInventory : ScriptableObject
{

    [SerializeField] public string Name;
    [SerializeField] public Sprite icon;
    [SerializeField]  public string IconPatch;  
    [SerializeField]  [Multiline] public string Description;    

    public abstract bool UseItem();
}

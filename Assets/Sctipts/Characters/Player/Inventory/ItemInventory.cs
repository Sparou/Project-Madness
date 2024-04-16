using UnityEngine;
public enum TypeInventory
{
    Gun,
    health,
    other
}



[CreateAssetMenu(fileName="New Item", menuName ="item/Create New Item")]
    public class ItemInventory : ScriptableObject
{

    [SerializeField] public string Name;
    [SerializeField] public int Id;
    [SerializeField] public int Count;
    [SerializeField] public Sprite icon;
    public GameObject Prefab;
    [SerializeField]  public string IconPatch;  
    [SerializeField]  [Multiline] public string Description;    
    [SerializeField] public TypeInventory Type;         

}

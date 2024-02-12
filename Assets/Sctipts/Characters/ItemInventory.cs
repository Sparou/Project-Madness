using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeInventory
{
    Gun,
    health,
    other
}



    public class ItemInventory : MonoBehaviour
{

    [SerializeField]  public string Name;
    [SerializeField]  public int Id;     
    [SerializeField]  public string IconPatch;  
    [SerializeField]  [Multiline] public string Description;    
    [SerializeField] public TypeInventory type;
    [SerializeField]  public int HealthRecovery;     
    [SerializeField]  public int Damage;      


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] string characterName;
    [SerializeField] float maxHealth;
    protected float currentHealth;

    [SerializeField] private int _humanPoints
    {
        get { return _humanPoints; }
        set { _humanPoints = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

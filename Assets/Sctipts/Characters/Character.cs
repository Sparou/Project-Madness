using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    [SerializeField] float maxHealth;
    protected float currentHealth;

    [SerializeField] private int _humanPoints;
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePoints(int ammount)
    {
        this._humanPoints = ammount;
    }

}

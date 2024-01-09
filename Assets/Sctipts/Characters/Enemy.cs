using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : NPC
{

    [SerializeField] float agressiveRadius;
    [SerializeField] float moveSpeed;

    [SerializeField] Player target;

    private float distanceToTarget;

    void Start()
    {

    }

    
    void Update()
    {
        Chase();
    }


    private void Chase()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= agressiveRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}

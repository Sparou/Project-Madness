using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackTrigger : MonoBehaviour
{

    private Enemy enemy;
    private AIAttackingTarget attackingTarget;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        attackingTarget = GetComponentInParent<AIAttackingTarget>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On trigger enter!");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("On player enter in trigger!");
            enemy.GetTarget().TakeDamge(attackingTarget.attackDamage);
        }
    }
}

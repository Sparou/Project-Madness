using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDistanceAttackTrigger : MonoBehaviour
{

    private Projectile projectile;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On trigger enter!");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("On player enter in trigger!");
            projectile.GetTarget().healthController.TakeDamage(projectile.damage);
            Destroy(projectile.gameObject);
        }
    }

}

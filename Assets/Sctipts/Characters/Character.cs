using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] string characterName = "Character";
    [SerializeField] float maxHealth = 100.0f;

    protected float currentHealth;
    protected bool isDead = false;

    //Components
    Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    public void TakeDamge(float damage)
    {
        Debug.Log(string.Format("{0} was damaged!", characterName));

        // Hurt animation
        // animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(string.Format("{0} was died!", characterName));

        // Die animation
        // animator.SetTrigger("Die")

        // Disable collision and logics
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}

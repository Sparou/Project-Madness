using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .5f;
    [SerializeField] private float attackCooldown = .1f;

    private Animator animator;
    private bool isAttacking;
    private float attackCooldownCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttack(InputValue value)
    {
        if (!isAttacking)
        {
            animator.SetTrigger("AttackTrigger");
            isAttacking = true;

            Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

            foreach (Collider2D character in hitCharacters)
            {
                //TODO: damage
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            attackCooldownCounter += Time.deltaTime;
            if (attackCooldownCounter >= attackCooldown)
            {
                isAttacking = false;
                attackCooldownCounter = 0;
            }
        }
    }

        private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

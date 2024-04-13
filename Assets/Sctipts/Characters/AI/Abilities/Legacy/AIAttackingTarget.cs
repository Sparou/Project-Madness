using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Enemy))]
public class AIAttackingTarget : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] float attackRadius;
    [SerializeField] public float attackDamage;
    [SerializeField] float attackCooldown;
    [SerializeField] bool canMoveInAttack;
    #endregion

    #region Private Variables
    private float attackCooldownTimer;

    private Enemy enemy;
    private Animator animator;
    #endregion

    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        attackCooldownTimer += Time.deltaTime;

        if (enemy.DistanceToTarget <= attackRadius && attackCooldownTimer >= attackCooldown)
        {
            AttackTarget();
        }
    }

    void AttackTarget()
    {
        attackCooldownTimer = 0;
        animator.SetTrigger("isAttacking");
    }
}

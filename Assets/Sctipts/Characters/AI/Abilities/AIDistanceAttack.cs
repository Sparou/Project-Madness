using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class AIDistanceAttack : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] float attackRadius;
    [SerializeField] public Projectile projectileType;
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

    // Update is called once per frame
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
        SpawnProjectile();
    }

    void SpawnProjectile()
    {
        Vector3 spawnPosition = transform.position;
        Instantiate(projectileType, spawnPosition, Quaternion.identity, transform);
    }
}

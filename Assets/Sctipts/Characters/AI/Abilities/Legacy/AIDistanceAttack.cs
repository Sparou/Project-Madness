using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class AIDistanceAttack : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] float attackRadius;
    [SerializeField] float attackCooldown;
    [SerializeField] float delayBeforeAttack;
    [SerializeField] bool canMoveInAttack;
    [SerializeField] public Projectile projectileType;

    [SerializeField] Transform projectileSpawnPositionUp;
    [SerializeField] Transform projectileSpawnPositionRight;
    [SerializeField] Transform projectileSpawnPositionLeft;
    [SerializeField] Transform projectileSpawnPositionDown;

    public bool isUpAttack;
    public bool isRightAttack;
    public bool isLeftAttack;
    public bool isDownAttack;

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
            StartCoroutine(nameof(AttackTargetWithDelay));
        }
    }

    IEnumerator AttackTargetWithDelay()
    {
        attackCooldownTimer = 0;
        animator.SetTrigger("isAttacking");
        yield return new WaitForSeconds(delayBeforeAttack);
        SpawnProjectile();
    }

    void SpawnProjectile()
    {
        Vector3 spawnPosition = transform.position;

        float Horizontal = animator.GetFloat("Horizontal");
        float Vertical = animator.GetFloat("Vertical");

        if (Mathf.Abs(Vertical) > Mathf.Abs(Horizontal)) 
        {
            if (isUpAttack) spawnPosition = projectileSpawnPositionUp.position;
            else spawnPosition = projectileSpawnPositionDown.position;
        }
        else
        {
            if (isRightAttack) spawnPosition = projectileSpawnPositionRight.position;
            else spawnPosition = projectileSpawnPositionLeft.position;
        }

        Instantiate(projectileType, spawnPosition, Quaternion.identity, null);
    }
}

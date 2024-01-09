using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor.Search;
using UnityEngine.Rendering;

[RequireComponent (typeof(Seeker))]
public class SkeletonAI : MonoBehaviour
{
    #region Main Variables
    [SerializeField] Player target;
    [SerializeField] float agressiveRadius;
    [SerializeField] float moveSpeed;

    private float currentMoveSpeed;
    #endregion

    #region Pathfinder Variables
    [SerializeField] float nextWaypointDistance;

    private float distanceToTarget;
    private int currentWaypoint = 0;
    private Path path;
    #endregion

    #region Attack Variables
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRadius;
    [SerializeField] float attackDamage;

    private CircleCollider2D attackTriggerColider;
    private RaycastHit2D attackHit;
    private float nextAttackTime;
    private bool inAttackRange;
    private float attackCooldownCounter;
    #endregion

    #region Components
    [SerializeField] Animator animator;

    private Seeker seeker;
    private Rigidbody2D rb;
    #endregion

    void Start()
    {
        currentMoveSpeed = moveSpeed;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        attackTriggerColider = GetComponentInChildren<CircleCollider2D>();
        attackTriggerColider.radius = attackRadius;
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
        attackCooldownCounter += Time.deltaTime;

        if (distanceToTarget <= agressiveRadius) ChaseTarget();
        if (distanceToTarget <= attackRadius && attackCooldownCounter >= attackCooldown) AttackTarget();

    }

    void ChaseTarget()
    {
        if (distanceToTarget > agressiveRadius) return;

        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count) return;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * currentMoveSpeed * Time.deltaTime;

        rb.AddForce(force);

        distanceToTarget = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distanceToTarget < nextWaypointDistance) currentWaypoint++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target.TakeDamge(attackDamage);
        }
    }

    void AttackTarget()
    {
        animator.SetBool("isAttacking", true);
        currentMoveSpeed = 0;
        attackCooldownCounter = 0;
    }

}

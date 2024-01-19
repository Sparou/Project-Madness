using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor.Search;
using UnityEngine.Rendering;

[RequireComponent(typeof(Seeker))]
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
    private bool reachedEndOfPath;
    #endregion

    #region Patrolling Variables
    [SerializeField] Transform[] patrollingWaypoints;
    [SerializeField] float waitTime = 15f;

    private int currentPatrollingWaypoint = 0;
    private bool agressiveStatus = false;
    private bool isWaiting = false;
    #endregion

    #region Attack Variables
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRadius;
    [SerializeField] float attackDamage;

    private BoxCollider2D attackTriggerColider;
    private bool inAttackRange;
    private float attackCooldownTimer = 0;
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
        attackTriggerColider = GetComponentInChildren<BoxCollider2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        attackTriggerColider.enabled = false;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (!agressiveStatus)
            {
                seeker.StartPath(rb.position, patrollingWaypoints[currentPatrollingWaypoint].position, OnPathComplete);
            }
            else
            {
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
            }
        }
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
        attackCooldownTimer += Time.deltaTime;
        attackCooldownTimer += 1;

        if (distanceToTarget > agressiveRadius && !isWaiting)
        {
            agressiveStatus = false;
            Patrol();
        }
        else if (distanceToTarget <= agressiveRadius)
        {
            agressiveStatus = true;
            ChaseTarget();
        }
        else if (distanceToTarget <= attackRadius && attackCooldownTimer >= attackCooldown)
        {   
            AttackTarget();
        }
    }

    void ChaseTarget()
    {

        isWaiting = false;

        if (distanceToTarget > agressiveRadius) return;

        if (path == null) return;

        currentMoveSpeed = moveSpeed;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            currentPatrollingWaypoint = (currentPatrollingWaypoint + 1) % patrollingWaypoints.Length;
            return;
        }

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
        attackTriggerColider.enabled = false;
    }

    void Patrol()
    {
        if (distanceToTarget <= agressiveRadius) return;

        if (path == null) return;

        currentMoveSpeed = moveSpeed / 2;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            currentPatrollingWaypoint = (currentPatrollingWaypoint + 1) % patrollingWaypoints.Length;
            isWaiting = true;
            currentMoveSpeed = 0;
            StartCoroutine(nameof(StopWait));
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * currentMoveSpeed * Time.deltaTime;

        rb.AddForce(force);

        var distanceToWaypoint = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distanceToWaypoint < nextWaypointDistance) currentWaypoint++;
    }
    IEnumerator StopWait()
    {
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
    void AttackTarget()
    {
        attackTriggerColider.enabled = true;
        animator.SetBool("isAttacking", true);
        currentMoveSpeed = 0;
        attackCooldownTimer = 0;
    }
}
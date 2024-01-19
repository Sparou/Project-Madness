using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Seeker))]
[RequireComponent (typeof(Rigidbody2D))]
public class ChasingTarget : MonoBehaviour
{
    #region Public Variables
    [SerializeField] float nextWaypointDistance;
    #endregion

    #region Private Variables
    private float distanceToTarget;
    private int currentWaypoint = 0;
    private Path path;

    private Seeker seeker;
    private Enemy enemy;
    private Rigidbody2D rb;
    #endregion

    void Start()
    {
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();

        enemy.SetCurrentMoveSpeed(enemy.GetMoveSpeed());
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void FixedUpdate()
    {   
        if (enemy.GetDistanceToTarget() <= enemy.GetAgressiveRadius())
        {
            enemy.SetAgressiveStatus(true);
            ChaseTarget();
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && enemy.GetAggressiveStatus())
        {
            seeker.StartPath(rb.position, enemy.GetTarget().transform.position, OnPathComplete);
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

    void ChaseTarget()
    {

        //isWaiting = false;

        if (distanceToTarget > enemy.GetAgressiveRadius()) return;

        if (path == null) return;

        enemy.SetCurrentMoveSpeed(enemy.GetMoveSpeed());

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * enemy.GetCurrentMoveSpeed() * Time.deltaTime;

        rb.AddForce(force);

        distanceToTarget = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distanceToTarget < nextWaypointDistance) currentWaypoint++;
    }

}

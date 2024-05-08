using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AIDestinationSetter))]
public class EnemyMovementController : MonoBehaviour
{
    [Header("Chasing")]
    public float chasingMoveSpeed;
    public float endReachedDistance;

    private Enemy owner;
    private AIPath aiPath;
    private AIDestinationSetter ds;
    private AIAbilityManager am;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        ds = GetComponent<AIDestinationSetter>();
        owner = GetComponent<Enemy>();
        am = GetComponent<AIAbilityManager>();

        aiPath.endReachedDistance = endReachedDistance;
        aiPath.maxSpeed = chasingMoveSpeed;
        ds.enabled = false;
        ds.target = owner.Target.transform;
    }

    void FixedUpdate()
    {
        if (!am.characterIsBusy && owner.DistanceToTarget <= owner.AgressiveRadius)
        {
            ds.enabled = true;
        } 
        else
        {
            ds.enabled = false;
        }
    }

    public void DisableAIMovement(AIAbilityManager am)
    {
        GetComponent<EnemyAnimatorController>().enabled = false;
        GetComponent<EnemyMovementController>().enabled = false;
        GetComponent<AIPath>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<Seeker>().enabled = false;
    }

    public void EnableAIMovement(AIAbilityManager am)
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<EnemyAnimatorController>().enabled = true;
        GetComponent<EnemyMovementController>().enabled = true;
        GetComponent<AIPath>().enabled = true;
        GetComponent<AIDestinationSetter>().enabled = true;
        GetComponent<Seeker>().enabled = true;
    }
}

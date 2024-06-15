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

    [Header("Patroling")]
    public float patrolingMoveSpeed;

    private Enemy owner;
    private AIPath aiPath;
    private AIDestinationSetter ds;
    private AIAbilityManager am;
    private Patrol patrol;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        ds = GetComponent<AIDestinationSetter>();
        owner = GetComponent<Enemy>();
        am = GetComponent<AIAbilityManager>();
        patrol = GetComponent<Patrol>();

        aiPath.endReachedDistance = endReachedDistance;
        aiPath.maxSpeed = chasingMoveSpeed;
        ds.enabled = false;
        patrol.enabled = false;
        ds.target = owner.Target.transform;
    }

    void FixedUpdate()
    {
        if (!am.characterIsBusy && owner.IsTargetVisible)
        {
            if (patrol.enabled == true) DisablePatroling();
            else ds.enabled = true;
        } 
        else if (am.characterIsBusy && owner.IsTargetVisible)
        {
            ds.enabled = false;
        }
        else if (!owner.IsTargetVisible && patrol.targets.Length > 0)
        {
            EnablePatroling();
        }
        
    }

    private void EnablePatroling()
    {
        ds.enabled = false;
        patrol.enabled = true;
        aiPath.maxSpeed = patrolingMoveSpeed;
        aiPath.endReachedDistance = 0.25f;
    }

    private void DisablePatroling()
    {
        patrol.enabled = false;
        ds.enabled = true;
        aiPath.maxSpeed = chasingMoveSpeed;
        aiPath.endReachedDistance = 2f;
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

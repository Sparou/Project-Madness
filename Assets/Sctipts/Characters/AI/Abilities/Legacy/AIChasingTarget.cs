using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public class AIChasingTarget : MonoBehaviour
{
    [Header("Chasing")]
    public float chasingMoveSpeed;
    public float endReachedDistance;

    private AIPath aiPath;
    private Enemy owner;
    private AIDestinationSetter ds;
    private AIAbilityManager am;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        ds = GetComponent<AIDestinationSetter>();
        owner = GetComponent<Enemy>();

        ds.enabled = false;

        ds.target = owner.Target.transform;
    }

    void FixedUpdate()
    {
        if (am != null && am.characterIsBusy)
        {
            if (owner.DistanceToTarget <= owner.VisionRadius) ChaseTarget();
            else ds.enabled = false;
        }
        else
        {
            if (owner.DistanceToTarget <= owner.VisionRadius) ChaseTarget();
            else ds.enabled = false;
        }
    }

    private void ChaseTarget()
    {
        aiPath.endReachedDistance = endReachedDistance;
        aiPath.maxSpeed = chasingMoveSpeed;
        ds.enabled = true;
    }

}

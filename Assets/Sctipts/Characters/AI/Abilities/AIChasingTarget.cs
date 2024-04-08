using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public class AIChasingTarget : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] float chasingMoveSpeed;
    [SerializeField] float endReachedDistance;
    #endregion

    #region Private Variables
    private AIPath aiPath;
    private Enemy enemy;
    private AIDestinationSetter destinationSetter;
    #endregion

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        enemy = GetComponent<Enemy>();

        destinationSetter.enabled = false;

        destinationSetter.target = enemy.Target.transform;
    }

    void FixedUpdate()
    {
        if (enemy.DistanceToTarget <= enemy.AgressiveRadius)
        {
            aiPath.endReachedDistance = endReachedDistance;
            aiPath.maxSpeed = chasingMoveSpeed;
            destinationSetter.enabled = true;
        }
        else
        {
            destinationSetter.enabled = false;
        }
    }
}

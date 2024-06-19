using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAnimatorController : AnimationController
{
    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;
    private Patrol patrol;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        patrol = GetComponent<Patrol>();
    }

    void Update()
    {
        Vector2 velocity = aiPath.velocity;
        Vector3 direction = Vector3.zero;

        if (aiDestinationSetter.enabled == true)
        {
            direction = (aiDestinationSetter.target.transform.position - transform.position).normalized;
        }
        else if (patrol.enabled == true && patrol.targets.Length > 0)
        {
            direction = (patrol.targets[patrol.Index].transform.position - transform.position).normalized;
        }
        
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", velocity.magnitude);
    }
}

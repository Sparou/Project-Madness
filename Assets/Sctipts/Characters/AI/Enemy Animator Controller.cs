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

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    void Update()
    {
        Vector2 velocity = aiPath.velocity;
        var direction = (aiDestinationSetter.target.transform.position - transform.position).normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", velocity.magnitude);
    }
}

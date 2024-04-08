using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;
    private AIPath AIPath;

    void Start()
    {
        AIPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 velocity = AIPath.velocity;

        if (velocity.magnitude != 0)
        {

            animator.SetFloat("Horizontal", velocity.normalized.x);
            animator.SetFloat("Vertical", velocity.normalized.y);
        }
        animator.SetFloat("Speed", velocity.magnitude);
    }
}

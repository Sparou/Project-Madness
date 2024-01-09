using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class AnimatorController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector2 velocity = rb.velocity;

        if (velocity.magnitude > 0)
        {
            if (velocity.normalized.x > 0)
            {
                animator.SetBool("isMovingRight", true);
                animator.SetBool("isMovingLeft", false);
            }

            if (velocity.normalized.x < 0)
            {
                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingLeft", true);
            }

            if (velocity.normalized.y > 0)
            {
                animator.SetBool("isMovingUp", true);
                animator.SetBool("isMovingDown", false);
            }

            if (velocity.normalized.y < 0)
            {
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", true);
            }

        }
    }
}

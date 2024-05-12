using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ~~ 1. Controls All Player Movement
    // ~~ 2. Updates Animator to Play Idle & Walking Animations

    private float speed = 4f;
    private Rigidbody2D myRigidbody;
    private Vector3 playerMovement;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerMovement = Vector3.zero;
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");

        UpdateAnimationAndMove();
    }

    private void UpdateAnimationAndMove()
    {
        if (playerMovement != Vector3.zero)
        {
            MoveCharacter();
            speed = 4f;
            animator.SetFloat("moveX", playerMovement.x);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Roll();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Atack();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Death();
        }
        if (Input.GetKey(KeyCode.LeftShift) && playerMovement != Vector3.zero)
        {
            speed = 5f;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
    private void Roll()
    {
        animator.SetTrigger("Roll");
    }
    private void Atack()
    {
        animator.SetTrigger("Attack");
    }
    private void Death()
    {
        animator.SetTrigger("Death");
    }
    private void Run()
    {
        animator.SetBool("Run", true);
    }
    private void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + playerMovement * speed * Time.deltaTime);
    }
}

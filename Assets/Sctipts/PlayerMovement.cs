using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;

    [SerializeField] float dashSpeed = 3;
    [SerializeField] float dashLength = .5f;
    [SerializeField] float dashCooldown = .1f;

    private float dashCounter;
    private float dashCooldownCounter;
    private float activeMoveSpeed;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    private bool isDashing = false;


    private Vector2 moveDirection;
    Animator animator;
    private bool stopped;

    void Start()
    {
        activeMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    //private void OnMove(InputValue value)
    //{
    //    moveInput = value.Get<Vector2>();

    //}

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();

        //TODO: сделать плавно
        if(moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
        }

        stopped = false;
        animator.ResetTrigger("IdleTrigger");
        animator.SetTrigger("MoveTrigger");
    }

    public void OnDash(InputValue value)
    {
        Debug.Log("DASH!");
        isDashing = true;
    }

    private void FixedUpdate()
    {
        //rb.velocity = moveInput * activeMoveSpeed;
        rb.velocity = moveDirection * activeMoveSpeed;

        //остановился
        if (rb.velocity.magnitude == 0f && !stopped)
        {
            stopped = true;
            animator.ResetTrigger("MoveTrigger");
            animator.SetTrigger("IdleTrigger");
        }

        if (isDashing)
        {

            if (dashCooldownCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                if (dashCounter <= 0)
                {
                    Debug.Log("Dash is finished!");
                    activeMoveSpeed = moveSpeed;
                    dashCooldownCounter = dashCooldown;
                }
            }

            if (dashCooldownCounter > 0)
            {
                dashCooldownCounter -= Time.deltaTime;

                if (dashCooldownCounter <= 0)
                {
                    dashCooldownCounter = 0f;
                    isDashing = false;
                }
            }
        }
    }
}
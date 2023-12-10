using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
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

    void Start()
    {
        activeMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnDash()
    {
        Debug.Log("DASH!");
        isDashing = true;
    }

    private void FixedUpdate()
    {   
        // Freeze player in dialogue
        if (DialogueManager.instance.dialogueIsPlaying)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = moveInput * activeMoveSpeed;

        if(isDashing)
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

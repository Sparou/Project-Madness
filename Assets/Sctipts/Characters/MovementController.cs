using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimationController))]
public class MovementController : MonoBehaviour
{
    private Character character;
    private Rigidbody2D characterRigidbody;
    private AnimationController animationController;

    private float dashCounter;
    private float dashCooldownCounter;
    private float activeMoveSpeed;
    private Vector2 moveInput;

    private bool isDashing = false;

    private Vector2 moveDirection;
    private bool stopped;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
        characterRigidbody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<AnimationController>();

        activeMoveSpeed = character.GetMoveSpeed();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();

        //TODO: сделать плавно
        if (moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
        }

        stopped = false;
    }

    public void OnDash(InputValue value)
    {
        isDashing = true;
    }

    private void FixedUpdate()
    {
        //rb.velocity = moveInput * activeMoveSpeed;
        //Debug.Log(moveDirection + " " + activeMoveSpeed);
        characterRigidbody.velocity = moveDirection * activeMoveSpeed;

        //остановился
        if (characterRigidbody.velocity.magnitude == 0f && !stopped)
        {
            stopped = true;
            animationController.OnMoveEnd();
        }

        if (isDashing)
        {

            if (dashCooldownCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = character.GetDashSpeed();
                dashCounter = character.GetDashLength();
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                if (dashCounter <= 0)
                {
                    Debug.Log("Dash is finished!");
                    activeMoveSpeed = character.GetMoveSpeed();
                    dashCooldownCounter = character.GetDashCooldown();
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

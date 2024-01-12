using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimationController))]
public class MovementController : MonoBehaviour
{
    private Player player;
    private Rigidbody2D characterRigidbody;
    private AnimationController animationController;

    private float activeMoveSpeed;

    private bool isDashing = false;
    private float dashCooldownCounter;

    private Vector2 moveDirection;

    private void Start()
    {
        player = GetComponent<Player>();
        characterRigidbody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<AnimationController>();

        activeMoveSpeed = player.GetMoveSpeed();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

        //TODO: сделать плавно
        if (moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
        }
    }

    public void OnDash(float dashCooldown)
    {
        if (!isDashing && dashCooldownCounter >= dashCooldown)
        {
            isDashing = true;

            activeMoveSpeed = player.GetDashSpeed();
            Debug.Log("DAsh");

            //Отложенное выключение dash
            Invoke(nameof(StopDash), player.GetDashLength());
        }
    }

    private void StopDash()
    {
        activeMoveSpeed = player.GetMoveSpeed();
        dashCooldownCounter = 0;
        isDashing = false;
    }

    private void Update()
    {
        dashCooldownCounter += Time.deltaTime;
        characterRigidbody.velocity = moveDirection * activeMoveSpeed;
    }
}

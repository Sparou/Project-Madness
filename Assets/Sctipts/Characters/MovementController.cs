using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimationController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashDuration = .5f;
    [SerializeField] private float dashCooldown = 5f;

    [SerializeField] private float dodgeSpeed = 15;
    [SerializeField] private float dodgeDuration = .1f;
    [SerializeField] private float dodgeCooldown = 2f;

    private Rigidbody2D characterRigidbody;
    private AnimationController animationController;

    private float activeMoveSpeed;
    private Vector2 moveDirection;

    private bool isDashing = false;
    private float dashCooldownCounter;

    private bool isDodging = false;
    private float dodgeCooldownCounter;

    private void Start()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<AnimationController>();

        activeMoveSpeed = moveSpeed;
        dashCooldownCounter = dashCooldown;
        dodgeCooldownCounter = dodgeCooldown;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

        //Анимация ходьбы
        animationController.SetSpeed(activeMoveSpeed * IsWalking());

        //TODO: добавить анимацию разворота
        if (moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
        }
    }

    #region GPT version
    //public void OnDodge(InputAction.CallbackContext context)
    //{
    //    StartCoroutine(IncreaseSpeedCoroutine(() => isDodging, 12, () => dodgeCooldownCounter, 16, 5, (flag, counter) => { isDodging = flag; dodgeCooldownCounter = counter; }));
    //}

    //public void OnDash(InputAction.CallbackContext context)
    //{
    //    StartCoroutine(IncreaseSpeedCoroutine(() => isDashing, 12, () => dashCooldownCounter, 16, 5, (flag, counter) => { isDashing = flag; dashCooldownCounter = counter; }));
    //}

    //IEnumerator IncreaseSpeedCoroutine(Func<bool> getFlag, float cooldown, Func<float> getCounter, float newSpeed, float duration, Action<bool, float> setValues)
    //{
    //    IncreaseSpeed(getFlag, cooldown, getCounter, newSpeed, duration, setValues);
    //    yield return null; // Wait for the next frame
    //}

    //public void IncreaseSpeed(Func<bool> getFlag, float cooldown, Func<float> getCounter, float newSpeed, float duration, Action<bool, float> setValues)
    //{
    //    bool enableFlag = getFlag();
    //    float cooldownCounter = getCounter();

    //    if (!enableFlag && cooldownCounter >= cooldown)
    //    {
    //        enableFlag = true;
    //        activeMoveSpeed = newSpeed;
    //        StartCoroutine(ReturnNormalSpeed(() => setValues(false, 0), duration));
    //    }
    //}

    //IEnumerator ReturnNormalSpeed(Action onComplete, float delayTime)
    //{
    //    yield return new WaitForSeconds(delayTime);

    //    activeMoveSpeed = player.GetMoveSpeed();
    //    onComplete.Invoke();
    //}
    #endregion

    public void OnDodge()
    {
        IncreaseSpeed(ref isDodging,
                      dodgeCooldown,
                      ref dodgeCooldownCounter,
                      dodgeSpeed,
                      dodgeDuration,
                      (flag, counter) => { isDodging = flag; dodgeCooldownCounter = counter; });
    }

    public void OnDash()
    {
        IncreaseSpeed(ref isDashing,
                      dashCooldown,
                      ref dashCooldownCounter,
                      dashSpeed,
                      dashDuration,
                      (flag, counter) => { isDashing = flag; dashCooldownCounter = counter; });
    }

    private void IncreaseSpeed(ref bool enableFlag,
                              float cooldown,
                              ref float cooldownCounter,
                              float newSpeed,
                              float duration,
                              Action<bool, float> setValues)
    {
        if (!enableFlag && cooldownCounter >= cooldown)
        {
            enableFlag = true;
            activeMoveSpeed = newSpeed;
            StartCoroutine(ReturnNormalSpeed(() => setValues(false, 0), duration));
        }
    }

    private IEnumerator ReturnNormalSpeed(Action onComplete, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        activeMoveSpeed = moveSpeed;
        onComplete.Invoke();
    }

    private void Update()
    {
        dashCooldownCounter += Time.deltaTime;
        dodgeCooldownCounter += Time.deltaTime;

        characterRigidbody.velocity = moveDirection * activeMoveSpeed;
    }

    private int IsWalking()
    {
        return moveDirection.x != 0 || moveDirection.y != 0 ? 1 : 0; 
    }
}

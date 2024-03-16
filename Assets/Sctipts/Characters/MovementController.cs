using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static AnimationController;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(HealthController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashDuration = .5f;
    [SerializeField] private float dashCooldown = 5f;

    [SerializeField] private float dodgeSpeed = 15;
    [SerializeField] private float dodgeDuration = .1f;
    [SerializeField] private float dodgeCooldown = 2f;

    [SerializeField] private float rollSpeed = 10;
    [SerializeField] private float rollCooldown = 1f;


    private Rigidbody2D characterRigidbody;
    private AnimationController animationController;
    private HealthController healthController;

    private float activeMoveSpeed;
    private Vector2 moveDirection;

    private InputAction.CallbackContext delayedContext;

    private bool isRolling = false;
    private float rollCooldownCounter;

    private bool isDashing = false;
    private float dashCooldownCounter;

    private bool isDodging = false;
    private float dodgeCooldownCounter;

    private void Start()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<AnimationController>();
        healthController = GetComponent<HealthController>();

        activeMoveSpeed = moveSpeed;
        dashCooldownCounter = dashCooldown;
        dodgeCooldownCounter = dodgeCooldown;
        rollCooldownCounter = rollCooldown;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //Если игрок не перекатывается, меняем направление движения
        if (!isRolling)
        {
            moveDirection = context.ReadValue<Vector2>();
        }

        //Запоминаем последнее действие
        delayedContext = context;

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

    private int IsWalking()
    {
        return moveDirection.x != 0 || moveDirection.y != 0 ? 1 : 0;
    }

    private void Update()
    {
        dashCooldownCounter += Time.deltaTime;
        dodgeCooldownCounter += Time.deltaTime;
        rollCooldownCounter += Time.deltaTime;

        characterRigidbody.velocity = moveDirection * activeMoveSpeed;
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

    public void OnRollStart()
    {
        if(IsWalking() != 0 && !isRolling && rollCooldownCounter >= rollCooldown)
        {
            isRolling = true;
            healthController.invincible = true;
            animationController.RollAnimation();
        }
    }

    private void OnRoll()
    {
        activeMoveSpeed = rollSpeed;
    }

    private void OnRollEnd()
    {
        activeMoveSpeed = moveSpeed;
        isRolling = false;
        rollCooldownCounter = 0;
        OnMove(delayedContext);
        healthController.invincible = false;
    }

    #region Реализация увеличения скорости №1
    /* 
     * Увеличение скорости и ее возвращение к нормальному значению 
     * происходят из анимации
     * Cинхронизированно с анимацией
     */
    public void OnDodgeStart()
    {
        if (!isDodging && dodgeCooldownCounter >= dodgeCooldown)
        {
            isDodging = true;
            animationController.DodgeAnimation();
        }
    }

    private void OnDodge()
    {
        activeMoveSpeed = dodgeSpeed;
    }

    public void OnDodgeEnd()
    {
        activeMoveSpeed = moveSpeed;
        isDodging = false;
        dodgeCooldownCounter = 0;
    }
    #endregion

    #region Реализация увеличения скорости №2
    /* 
     * Функция IncreaseSpeed увеличивает скорость до заданной, 
     * а затем возвращает нормальную скорость через заданное время;
     * Никак не синхронизированно с анимацией
     */
    //private void OnDodge()
    //{
    //    IncreaseSpeed(ref isDodging,
    //                  dodgeCooldown,
    //                  ref dodgeCooldownCounter,
    //                  dodgeSpeed,
    //                  dodgeDuration,
    //                  (flag, counter) => { isDodging = flag; dodgeCooldownCounter = counter; });
    //}

    //private void OnDash()
    //{
    //    IncreaseSpeed(ref isDashing,
    //                  dashCooldown,
    //                  ref dashCooldownCounter,
    //                  dashSpeed,
    //                  dashDuration,
    //                  (flag, counter) => { isDashing = flag; dashCooldownCounter = counter; });
    //}

    //private void IncreaseSpeed(ref bool enableFlag,
    //                          float cooldown,
    //                          ref float cooldownCounter,
    //                          float newSpeed,
    //                          float duration,
    //                          Action<bool, float> setValues)
    //{
    //    if (!enableFlag && cooldownCounter >= cooldown)
    //    {
    //        enableFlag = true;
    //        activeMoveSpeed = newSpeed;
    //        StartCoroutine(ReturnNormalSpeed(() => setValues(false, 0), duration));
    //    }
    //}

    //private IEnumerator ReturnNormalSpeed(Action onComplete, float delayTime)
    //{
    //    yield return new WaitForSeconds(delayTime);

    //    activeMoveSpeed = moveSpeed;
    //    onComplete.Invoke();
    //}
    #endregion
}

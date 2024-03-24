using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
[RequireComponent (typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashDuration = .5f;
    [SerializeField] private float dashCooldown = 5f;

    [Header("Dodge")]
    [SerializeField] private float dodgeSpeed = 15;
    [SerializeField] private float dodgeDuration = .1f;
    [SerializeField] private float dodgeCooldown = 2f;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5;

    [Header("Roll")]
    [SerializeField] private float rollSpeed = 10;
    [SerializeField] private float rollCooldown = 1f;

    private Player player;
    private Rigidbody2D characterRigidbody;

    private float activeMoveSpeed;
    private Vector2 moveDirection;
    private Vector2 viewDirection = new(-1f, 0f);

    private float rollCooldownCounter;
    private float dodgeCooldownCounter;

    private void Start()
    {
        player = GetComponent<Player>();
        characterRigidbody = GetComponent<Rigidbody2D>();

        activeMoveSpeed = moveSpeed;
        rollCooldownCounter = rollCooldown;
    }

    public void OnDisable()
    {
        characterRigidbody.velocity = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //Если игрок не перекатывается, меняем направление движения
        if (!player.isRolling && !player.isAttacking)
        {
            moveDirection = context.ReadValue<Vector2>();
            //Запоминаем направление если оно ненулевое
            if (IsWalking() == 1)
            {
                viewDirection = moveDirection;
            }
        }
        //Запоминаем последнее действие
        player.moveLastContext = context;

        //Анимация ходьбы
        player.animationController.SetSpeed(activeMoveSpeed * IsWalking());

        //TODO: добавить анимацию разворота
        player.viewController.Turn(moveDirection);
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
    }

    private int IsWalking()
    {
        return moveDirection != Vector2.zero ? 1 : 0;
    }

    private void Update()
    {
        dodgeCooldownCounter += Time.deltaTime;
        rollCooldownCounter += Time.deltaTime;

        characterRigidbody.velocity = moveDirection * activeMoveSpeed;
    }

    public void OnRollStart()
    {
        if(!player.isAttacking && !player.isRolling && rollCooldownCounter >= rollCooldown)
        {
            player.animationController.RollAnimation();
        }
    }

    private void OnRoll()
    {
        //кувырок по направлению взгляда
        player.isRolling = true;
        player.healthController.invincible = true;
        moveDirection = viewDirection;
        player.viewController.Turn(moveDirection);
        activeMoveSpeed = rollSpeed;
    }

    private void OnRollEnd()
    {
        activeMoveSpeed = moveSpeed;
        player.isRolling = false;
        rollCooldownCounter = 0;
        OnMove(player.moveLastContext);
        player.healthController.invincible = false;
    }

    #region Реализация увеличения скорости №1
    /* 
     * Увеличение скорости и ее возвращение к нормальному значению 
     * происходят из анимации
     * Cинхронизированно с анимацией
     */
    public void OnDodgeStart()
    {
        if (!player.isDodging && dodgeCooldownCounter >= dodgeCooldown)
        {
            player.isDodging = true;
            player.animationController.DodgeAnimation();
        }
    }

    private void OnDodge()
    {
        activeMoveSpeed = dodgeSpeed;
    }

    public void OnDodgeEnd()
    {
        activeMoveSpeed = moveSpeed;
        player.isDodging = false;
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

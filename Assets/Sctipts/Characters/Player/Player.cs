using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(ViewController))]
public class Player : Character
{
    public AnimationController animationController;
    public AttackController attackController;
    public MovementController movementController;
    public PlayerInputActions playerInputActions;
    public ViewController viewController;

    public bool isAttacking = false;
    public bool isRolling = false;
    public bool isDodging = false;

    //Движение
    public InputAction.CallbackContext moveLastContext;

    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
        attackController = GetComponent<AttackController>();
        movementController = GetComponent<MovementController>();
        playerInputActions = new PlayerInputActions();
        viewController = GetComponent<ViewController>();

        #region Action functions binding
        playerInputActions.Player.Move.performed += context => movementController.OnMove(context);
        playerInputActions.Player.Move.canceled += context => movementController.OnMove(context);
        playerInputActions.Player.Dodge.started += context => movementController.OnDodgeStart();
        playerInputActions.Player.Roll.started += context => movementController.OnRollStart();
        playerInputActions.Player.Fire.started += context => attackController.OnFire();
        #endregion
    }
    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
        movementController.OnDisable();
    }

}

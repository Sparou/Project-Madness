using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(AttackController))]
public class Player : Character
{
    public PlayerInputActions playerInputActions { get; private set; }

    private MovementController movementController;
    private AnimationController animationController;
    private AttackController attackController;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        movementController = GetComponent<MovementController>();
        animationController = GetComponent<AnimationController>();
        attackController = GetComponent<AttackController>();

        #region Action functions binding
        
        #region Movement actions
        playerInputActions.Player.Move.performed += context => movementController.OnMove(context);
        playerInputActions.Player.Move.canceled += context => movementController.OnMove(context);
        playerInputActions.Player.Dash.started += context => movementController.OnDash(GetDashCooldown());
        playerInputActions.Player.Fire.started += context => attackController.OnFire(GetAttackCooldown());
        #endregion

        #endregion

        #region Animation functions binding

        #region Movement animations
        playerInputActions.Player.Move.started += context => animationController.OnMoveStart();
        playerInputActions.Player.Move.canceled += context => animationController.OnMoveEnd();
        #endregion

        #endregion
    }
    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    //DEBUG
    private void OnDrawGizmosSelected()
    {
        if (GetAttackPoint() == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(GetAttackPoint().position, GetAttackRange());
    }
}

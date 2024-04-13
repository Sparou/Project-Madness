using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(AttackController))]
public class Player : Character
{
    private PlayerInputActions playerInputActions;
    private MovementController movementController;
    private AttackController attackController;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        movementController = GetComponent<MovementController>();
        attackController = GetComponent<AttackController>();

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
    }

}

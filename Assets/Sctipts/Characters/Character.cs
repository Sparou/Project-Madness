using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class Character : MonoBehaviour
{
    [SerializeField] private string characterName;

    #region Health variables 
    [SerializeField] private float maxHealth;
    public float GetMaxHealth() { return maxHealth; }
    #endregion

    #region Movement variables
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float rotationSpeed = 3;
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetRotationSpeed() { return rotationSpeed; }

    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashDuration = .5f;
    [SerializeField] private float dashCooldown = 5f;
    public float GetDashSpeed() { return dashSpeed; }
    public float GetDashDuration() { return dashDuration; }
    public float GetDashCooldown() {  return dashCooldown; }

    [SerializeField] private float dodgeSpeed = 15;
    [SerializeField] private float dodgeDuration = .1f;
    [SerializeField] private float dodgeCooldown = 2f;
    public float GetDodgeSpeed() { return dodgeSpeed; }
    public float GetDodgeDuration() { return dodgeDuration; }
    public float GetDodgeCooldown() { return dodgeCooldown; }
    #endregion

    #region Attack variables
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRange = .5f;
    [SerializeField] protected float attackCooldown = .1f;
    ///<summary> 
    ///Сколько времени дается игроку, чтобы совершить следующую атаку в серии 
    ///</summary>*/
    [SerializeField] protected float nextAttackTimeLimit = .2f;
    #endregion

    public HealthController healthController;

    private void Start()
    {
        healthController = GetComponent<HealthController>();
    }
}

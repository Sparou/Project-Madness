using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(HealthController))]
public class Character : MonoBehaviour
{
    [SerializeField] private string characterName;
    [SerializeField] private LayerMask layerMask;
    public LayerMask GetLayerMask() { return layerMask; }

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
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .5f;
    [SerializeField] private float attackCooldown = .1f;
    ///<summary> 
    ///—колько времени даетс€ игроку, чтобы совершить следующую атаку в серии 
    ///</summary>*/
    [SerializeField] private float nextAttackTimeLimit = .2f;
    public Transform GetAttackPoint() { return attackPoint; }
    public float GetAttackRange() { return attackRange; }
    public float GetAttackCooldown() { return attackCooldown; }
    public float GetNextAttackTimeLimit() { return nextAttackTimeLimit; }
    #endregion

    //TODO: создать класс оружи€
    #region Weapon variables
    [SerializeField] private float weaponDamage = 5f;
    public float GetWeaponDamage() { return weaponDamage; }
    #endregion

    public HealthController healthController;

    private void Start()
    {
        healthController = GetComponent<HealthController>();
    }
}

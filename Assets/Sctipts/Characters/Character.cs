using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string characterName;

    #region Health variables 
    [SerializeField] private float maxHealth;
    public float currentHealth { get; private set; }
    #endregion

    #region Movement variables
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float dashSpeed = 3;
    [SerializeField] private float dashLength = .5f;
    [SerializeField] private float dashCooldown = .1f;
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetDashSpeed() { return dashSpeed; }
    public float GetDashLength() { return dashLength; }
    public float GetDashCooldown() {  return dashCooldown; }
    #endregion

    #region Attack variables
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .5f;
    [SerializeField] private float attackCooldown = .1f;
    public Transform GetAttackPoint() { return attackPoint; }
    public float GetAttackRange() { return attackRange; }
    public float GetAttackCooldown() {  return attackCooldown; }
    #endregion
}

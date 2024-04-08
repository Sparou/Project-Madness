using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : NPC
{
    #region Serialized Fields
    [SerializeField] Player target;
    [SerializeField] float agressiveRadius;
    #endregion

    #region Private Variables
    private bool isAttacking;
    private float distanceToTarget;
    #endregion

    private void FixedUpdate()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
    }

    #region Getters
    public bool IsAttacking => isAttacking;
    public Player Target => target;
    public float DistanceToTarget => distanceToTarget;
    public float AgressiveRadius => agressiveRadius;

    public Player GetTarget() { return target; }
    public float GetDistanceToTarget() { return distanceToTarget; }
    public bool GetIsAttacking() { return isAttacking; }
    #endregion

    #region Setters
    public void SetIsAttacking(bool value) { isAttacking = value; }
    #endregion

}

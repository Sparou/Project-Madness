using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : NPC
{
    public Player target;
    public float visionRadius;
    public LayerMask wallLayer;

    #region Private Variables
    private bool isAttacking;
    private bool isTargetVisible;
    private float distanceToTarget;
    #endregion

    private void Awake()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
    }

    private void FixedUpdate()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
        CheckTargetVisibility();
    }

    private void CheckTargetVisibility()
    {
        isTargetVisible = false;

        Vector3 directionToTarget = target.transform.position - transform.position;

        if (distanceToTarget <= visionRadius)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget.normalized, distanceToTarget, wallLayer);
            if (!hit.collider)
            {
                isTargetVisible = true;
            }
            Debug.DrawRay(transform.position, directionToTarget.normalized * distanceToTarget, isTargetVisible ? Color.green : Color.red);
        }
    }

    #region Getters
    public bool IsAttacking => isAttacking;
    public Player Target => target;
    public float DistanceToTarget => distanceToTarget;
    public float VisionRadius => visionRadius;
    public bool IsTargetVisible => isTargetVisible;
    #endregion

    #region Setters
    public void SetIsAttacking(bool value) { isAttacking = value; }
    #endregion

}

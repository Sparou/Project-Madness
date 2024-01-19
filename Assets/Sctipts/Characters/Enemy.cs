using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField] Player target;
    [SerializeField] float agressiveRadius;
    [SerializeField] float moveSpeed;

    private float currentMoveSpeed;
    private bool agressiveStatus;
    private float distanceToTarget;

    private void FixedUpdate()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
    }

    public float GetAgressiveRadius()
    {
        return agressiveRadius;
    }

    public bool GetAggressiveStatus()
    {
        return agressiveStatus;
    }

    public void SetAgressiveStatus(bool value)
    {
        agressiveStatus = value;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetCurrentMoveSpeed()
    {
        return currentMoveSpeed;
    }

    public void SetCurrentMoveSpeed(float value)
    {
        currentMoveSpeed = value;
    }

    public Player GetTarget()
    {
        return target;
    }

    public float GetDistanceToTarget()
    {
        return distanceToTarget;
    }

}

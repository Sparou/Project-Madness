using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Attack", menuName = "Abilities/LongRangeAtack")]
public class AILongRangeAttackAbility : AIAbility
{
    [Header("Long Range Attack")]
    public Projectile projectile;
    public float minRange;
    public float maxRange;
    public float delay;
    public string animationTriggerName;

    [Header("Projectile Spawn Shifts")]
    public Vector3 ShiftUp;
    public Vector3 ShiftRight;
    public Vector3 ShiftLeft;
    public Vector3 ShiftDown;

    public override bool CheckActivationCondition(AIAbilityManager am)
    {
        float distanceToTarget = am.GetComponent<Enemy>().DistanceToTarget;
        if (distanceToTarget >= minRange && distanceToTarget <= maxRange)
        {
            Activate(am);
            return true;
        }
        return false;
    }

    public override void Activate(AIAbilityManager am)
    {
        Debug.Log(am.gameObject.ToString() + "activate " + abilityName);
        Animator animator = am.gameObject.GetComponent<Animator>();
        animator.SetTrigger(animationTriggerName);
        am.StartCoroutine(AttackTargetWithDelay(am));
        base.Activate(am);
    }

    IEnumerator AttackTargetWithDelay(AIAbilityManager am)
    {
        yield return new WaitForSeconds(delay);
        SpawnProjctile(am);
    }

    void SpawnProjctile(AIAbilityManager am)
    {
        Animator animator = am.GetComponent<Animator>();
        Vector3 spawnPosition = am.transform.position;

        float Horizontal = animator.GetFloat("Horizontal");
        float Vertical = animator.GetFloat("Vertical");

        if (Mathf.Abs(Vertical) > Mathf.Abs(Horizontal))
        {
            if (Vertical > 0) spawnPosition += ShiftUp;
            else spawnPosition += ShiftDown;
        }
        else
        {
            if (Horizontal > 0) spawnPosition += ShiftRight;
            else spawnPosition += ShiftLeft;
        }

        Instantiate(projectile, spawnPosition, Quaternion.identity, null);
    }
}

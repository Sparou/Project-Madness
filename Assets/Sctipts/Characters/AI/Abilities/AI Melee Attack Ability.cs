using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Attack", menuName = "Abilities/MeleeAtack")]
public class AIMeleeAttackAbility : AIAttackAbility
{
    [Header("Melee Attack")]
    public float minRange;
    public float maxRange;

    [Header("Animations")]
    public string animationTriggerName;

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
        Animator animator = am.gameObject.GetComponent<Animator>();
        animator.SetTrigger(animationTriggerName);
        base.Activate(am);
    }

}

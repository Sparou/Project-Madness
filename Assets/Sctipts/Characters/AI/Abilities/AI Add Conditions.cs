using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Add Condition Ability", menuName = "Abilities/AddCondition")]
public class AIAddConditions : AIAbility
{
    public float triggerDistance;
    public Condition[] conditions;

    [Header("Animations")]
    public string animationTriggerName;

    public override bool CheckActivationCondition(AIAbilityManager am)
    {
        float distanceToTarget = am.GetComponent<Enemy>().DistanceToTarget;
        if (distanceToTarget <= triggerDistance)
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

    protected override IEnumerator OnAbilityEnd(AIAbilityManager am)
    {
        yield return new WaitForSeconds(duration);
        float distanceToTarget = am.GetComponent<Enemy>().DistanceToTarget;
        if (distanceToTarget <= triggerDistance)
        {
            foreach (var condition in conditions)
            {
                am.GetComponent<Enemy>().Target.GetComponent<ConditionManager>().AddCondition(condition);
            }
        }
    }
}

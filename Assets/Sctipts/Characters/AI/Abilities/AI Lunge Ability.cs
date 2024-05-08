using Pathfinding;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "Lunge", menuName = "Abilities/Lunge")]
public class AILungeAbility : AIAttackAbility
{
    [Header("Lunge")]
    public float triggerDistance;

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
        am.GetComponent<AIPath>().endReachedDistance = triggerDistance;

        Animator animator = am.gameObject.GetComponent<Animator>();
        animator.SetTrigger(animationTriggerName);

        am.GetComponent<EnemyMovementController>().DisableAIMovement(am);
        base.Activate(am);
    }

    protected override IEnumerator OnAbilityEnd(AIAbilityManager am)
    {
        yield return new WaitForSeconds(duration);
        am.GetComponent<EnemyMovementController>().EnableAIMovement(am);
        am.GetComponent<AIPath>().endReachedDistance = am.GetComponent<EnemyMovementController>().endReachedDistance;
    }

}

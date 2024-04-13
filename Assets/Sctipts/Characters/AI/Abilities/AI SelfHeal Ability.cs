using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfHeal", menuName = "Abilities/SelfHeal")]
public class AISelfHealAbility : AIAbility
{
    public float healPower;
    public float upperHealthBorder;
    public string animationTriggerName;
    private HealthController hc;

    public override bool CheckActivationCondition(AIAbilityManager am)
    {
        hc = am.gameObject.GetComponent<HealthController>();
        if (hc != null && hc.currentHealth <= upperHealthBorder)
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
        hc.TakeHeal(healPower);
        base.Activate(am);
    }
}

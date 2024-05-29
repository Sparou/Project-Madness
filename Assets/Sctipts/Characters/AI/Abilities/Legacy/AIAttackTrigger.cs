using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackTrigger : MonoBehaviour
{
    public Condition[] conditions;

    private Enemy owner;
    private AIAbilityManager am;

    private void Start()
    {
        owner = GetComponentInParent<Enemy>();
        am = GetComponentInParent<AIAbilityManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(gameObject.ToString() + " collision with Player");
            AIAttackAbility ability = (AIAttackAbility)am.abilities[am.currentAbility];
            owner.Target.healthController.TakeDamage(ability.damage);
            ApplyConditions();
        }
    }

    private void ApplyConditions()
    {
        foreach (var condition in conditions)
        {
            owner.Target.GetComponent<ConditionManager>().AddCondition(condition);
        }
    }
}

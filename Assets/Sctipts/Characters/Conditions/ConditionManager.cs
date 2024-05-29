using NUnit.Framework;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public List<Condition> conditions;
    private List<float> timers;

    private float currentContinuousDamage;
    private HealthController hc;

    private void Start()
    {
        hc = GetComponent<HealthController>();
        currentContinuousDamage = 0;
        conditions = new List<Condition>();
        timers = new List<float>();
    }

    private void Update()
    {
        UpdateTimers();
        ApplyConituousDamage();
        DeleteEndedCondition();
    }

    public void AddCondition(Condition condition)
    {
        if (!CheckEqualConditionTypes(condition))
        {
            conditions.Add(condition);
            timers.Add(0);

            switch (condition.type)
            {
                case ConditionType.Stun:
                    StunStatusChange(true); break;
                case ConditionType.Bleeding:
                    OnBleedingAdded(condition); break;
                default:
                    break;
            }
        }
    }

    private void StunStatusChange(bool status)
    {
        Player player = GetComponent<Player>();
        Enemy enemy = GetComponent<Enemy>();

        if (player != null)
        {
            GetComponent<Player>().enabled = !status;
        } 
        else if (enemy != null)
        {
            GetComponent<EnemyMovementController>().enabled = !status;
            GetComponent<AIDestinationSetter>().enabled = !status;
            GetComponent<AIPath>().enabled = !status;
            GetComponent<Seeker>().enabled = !status;
            GetComponent<AIAbilityManager>().enabled = !status;
        }
    }

    private void OnBleedingAdded(Condition condition)
    {
        currentContinuousDamage += ((ContinuousDamageCondition)condition).damagePerSecond;
    }

    private void OnBleedingEnded(Condition condition)
    {
        currentContinuousDamage -= ((ContinuousDamageCondition)condition).damagePerSecond;
    }

    private void UpdateTimers()
    {
        if (timers.Count > 0)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                timers[i] += Time.deltaTime;
            }
        }
    }

    private void DeleteEndedCondition() 
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (timers[i] >= conditions[i].duration)
            {
                switch (conditions[i].type)
                {
                    case ConditionType.Stun:
                        StunStatusChange(false); break;
                    case ConditionType.Bleeding:
                        OnBleedingEnded(conditions[i]); break;
                    default: 
                        break;
                }

                conditions.Remove(conditions[i]);
                timers.Remove(timers[i]);
            }
        }
    }

    public void ApplyConituousDamage()
    {
        hc.TakeSlightDamage(currentContinuousDamage * Time.deltaTime);
    }

    private bool CheckEqualConditionTypes(Condition condition)
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].type == condition.type)
            {
                switch(condition.type)
                {
                    case ConditionType.Stun:
                        conditions[i].duration = condition.duration;
                        timers[i] = 0;
                        break;
                    case ConditionType.Bleeding:
                        return false;
                    default: break;
                }
            }
        }
        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Playables;
using UnityEngine;

public class AIAbilityManager : MonoBehaviour
{
    public AIAbility[] abilities;

    [HideInInspector]
    public bool characterIsBusy;

    [HideInInspector]
    public int currentAbility;

    private float[] lastUsesTime;

    private void Start()
    {
        characterIsBusy = false;
        ResetAllAbilities();
    }

    private void Update()
    {
        if (!characterIsBusy)
        {
            for (int abilityIdx = 0; abilityIdx < abilities.Length; abilityIdx++) 
            {
                if (!characterIsBusy && (lastUsesTime[abilityIdx] == 0 || Time.time - lastUsesTime[abilityIdx] >= abilities[abilityIdx].cooldownTime) &&
                    abilities[abilityIdx].CheckActivationCondition(this))
                {
                    currentAbility = abilityIdx;
                    lastUsesTime[abilityIdx] = Time.time;
                    characterIsBusy = true;
                    Invoke(nameof(OnAbilityEnd), abilities[abilityIdx].duration);
                }
            }
        }
    }

    private void AddForceToRB(float force)
    {
        Vector2 direction = GetComponent<Enemy>().Target.transform.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Force);
    }

    private void OnAbilityEnd()
    {
        characterIsBusy = false;
    }

    private void ResetAllAbilities()
    {
        lastUsesTime = new float[abilities.Length];
        for (int i = 0; i < abilities.Length; i++) 
        {
            lastUsesTime[i] = 0f;
        }
    }
}

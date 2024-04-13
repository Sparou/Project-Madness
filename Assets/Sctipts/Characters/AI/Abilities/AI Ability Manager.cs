using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class AIAbilityManager : MonoBehaviour
{
    public AIAbility[] abilities;

    [HideInInspector]
    public bool characterIsBusy;

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
                if (Time.time - lastUsesTime[abilityIdx] >= abilities[abilityIdx].cooldownTime &&
                    abilities[abilityIdx].CheckActivationCondition(this))
                {
                    lastUsesTime[abilityIdx] = Time.time;
                    characterIsBusy = true;
                    Invoke(nameof(OnAbilityEnd), abilities[abilityIdx].duration);
                }
            }
        }
    }

    private void OnAbilityEnd()
    {
        characterIsBusy = false;
    }

    private void ResetAllAbilities()
    {
        for (int i = 0; i < abilities.Length; i++) 
        {
            lastUsesTime = new float[abilities.Length];
            lastUsesTime[i] = 0f;
        }
    }
}

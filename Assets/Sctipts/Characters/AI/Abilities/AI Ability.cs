using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[System.Serializable]
public class AIAbility : ScriptableObject
{
    [Header("Ability")]
    public string abilityName;
    public float cooldownTime;
    public float duration;

    [HideInInspector]
    public float lastUseTime = 0f;

    public virtual bool CheckActivationCondition(AIAbilityManager am) 
    {
        return true;
    }

    public virtual void Activate(AIAbilityManager am) 
    {
        Debug.Log(abilityName + " was activated!");
        am.characterIsBusy = true;
        lastUseTime = Time.time;
    }
}

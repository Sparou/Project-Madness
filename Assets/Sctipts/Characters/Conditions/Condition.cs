using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    Stun,
    Bleeding,
    Poisoning,
    Curse
}

[System.Serializable]
[CreateAssetMenu(fileName = "Condition", menuName = "Conditions/Condition")]
public class Condition : ScriptableObject
{
    [Header("Condition")]
    public ConditionType type;
    public float duration;
}

[CreateAssetMenu(fileName = "ContinuousDamageCondition", menuName = "Conditions/ContinuousDamageCondition")]
public class ContinuousDamageCondition : Condition
{
    [Header("Continuous Damage")]
    public float damagePerSecond;
}


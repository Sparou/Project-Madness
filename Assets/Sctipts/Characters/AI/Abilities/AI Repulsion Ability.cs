using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum RepulsionType
{
    Targeted,
    Circular
}

[CreateAssetMenu(fileName = "Repulsion", menuName = "Abilities/Repulsion")]
public class AIRepulsionAbility : AIAbility
{
    [Header("Repulsion")]
    public float triggerDistance;

    private Enemy owner;
}

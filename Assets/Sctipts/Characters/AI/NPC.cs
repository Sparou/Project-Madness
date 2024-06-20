using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    [SerializeField] RelationshipStatus relationshipStatus;
}

enum RelationshipStatus
{
    FRIENDLY,
    NEUTRAL,
    ENEMY
}
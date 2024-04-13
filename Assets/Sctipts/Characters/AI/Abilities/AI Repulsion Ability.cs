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
    [Tooltip ("Дистанция по достижении которой будет срабатывать отбрасывание")]
    public float triggerDistance;
    public float repulsionForce;
    [Tooltip ("Targeted - отбрасывает только игрока;\nCicrular - отбрасывает всех в определенном радиусе.")]
    public RepulsionType repulsionType;
    public float repuslionRadius;
    public int layerMaskIdx;

    private Enemy owner;

    public override bool CheckActivationCondition(AIAbilityManager am)
    {
        owner = am.GetComponent<Enemy>();
        if (owner.DistanceToTarget <= triggerDistance)
        {
            Activate(am);
            return true;
        }
        return false;
    }

    public override void Activate(AIAbilityManager am)
    {
        if (repulsionType == RepulsionType.Targeted) 
        {
            Vector3 direction = (owner.Target.transform.position - owner.transform.position);
            //owner.Target.GetComponent<Rigidbody2D>().AddForce(direction * repulsionForce);
            //owner.Target.GetComponent<Rigidbody2D>().velocity = direction * repulsionForce;
            //owner.Target.GetComponent<Rigidbody2D>().MovePosition(owner.transform.position + direction * repulsionForce);
            //owner.Target.GetComponent<Rigidbody2D>().
        }
        else if (repulsionType == RepulsionType.Circular)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(owner.transform.position, repuslionRadius);
            foreach (Collider2D target in targets)
            {
                if (target.gameObject.layer == LayerMask.NameToLayer("Characters") && target.gameObject != am.gameObject) 
                {
                    Vector3 direction = (owner.Target.transform.position - owner.transform.position).normalized;
                    target.GetComponent<Rigidbody2D>().AddForce(direction * repulsionForce, ForceMode2D.Impulse);
                    target.GetComponent<Rigidbody2D>().velocity = direction * repulsionForce;
                    target.GetComponent<Rigidbody2D>().MovePosition(owner.transform.position + direction * repulsionForce);
                }

            }
        }
        base.Activate(am);
    }
}

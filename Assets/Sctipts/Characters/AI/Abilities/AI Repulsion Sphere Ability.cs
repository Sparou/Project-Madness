using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RepulsionSphere", menuName = "Abilities/RepulsionSphere")]
public class AIRepulsionSphereAbility : AIAbility
{
    [Header("Repulsion")]
    public float triggerDistance;
    public Projectile projectile;
    public float delay;
    public string animationTriggerName;

    private Enemy owner;

    [Header("Projectile Spawn Shifts")]
    public Vector3 ShiftUp;
    public Vector3 ShiftRight;
    public Vector3 ShiftLeft;
    public Vector3 ShiftDown;

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
        Debug.Log(am.gameObject.ToString() + "activate " + abilityName);
        Animator animator = am.gameObject.GetComponent<Animator>();
        animator.SetTrigger(animationTriggerName);
        am.StartCoroutine(AttackTargetWithDelay(am));
        base.Activate(am);
    }

    protected IEnumerator AttackTargetWithDelay(AIAbilityManager am)
    {
        yield return new WaitForSeconds(delay);
        SpawnProjctile(am);
    }

    protected void SpawnProjctile(AIAbilityManager am)
    {
        Animator animator = am.GetComponent<Animator>();
        Vector3 spawnPosition = am.transform.position;

        float Horizontal = animator.GetFloat("Horizontal");
        float Vertical = animator.GetFloat("Vertical");

        if (Mathf.Abs(Vertical) > Mathf.Abs(Horizontal))
        {
            if (Vertical > 0) spawnPosition += ShiftUp;
            else spawnPosition += ShiftDown;
        }
        else
        {
            if (Horizontal > 0) spawnPosition += ShiftRight;
            else spawnPosition += ShiftLeft;
        }

        Instantiate(projectile, spawnPosition, Quaternion.identity, null);
    }
}

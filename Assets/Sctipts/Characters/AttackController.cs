using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(AnimationController))]
public class AttackController : MonoBehaviour
{
    private Character character;
    private AnimationController animationController;

    private float attackCooldownCounter = 0;
    public bool isAttacking { get; private set; } = false;

    // Start is called before the first frame update
    private void Start()
    {
        character = GetComponent<Character>();
        animationController = GetComponent<AnimationController>();
    }

    public void OnFire(InputValue value)
    {
        //TODO: урон не повтор€ютс€
        if (!isAttacking && attackCooldownCounter >= character.GetAttackCooldown())
        {
            animationController.FireAnimation();
            isAttacking = true;

            //Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(character.attackPoint.position, character.attackRange);

            //foreach (Collider2D character in hitCharacters)
            //{
            //    //TODO: damage
            //}

            attackCooldownCounter = 0;
            isAttacking = false;
        }
    }

    private void Update()
    {
        attackCooldownCounter += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (character.GetAttackPoint() == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(character.GetAttackPoint().position, character.GetAttackRange());
    }
}

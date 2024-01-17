using UnityEngine;
using static AnimationController;

[RequireComponent(typeof(AnimationController))]
public class AttackController : MonoBehaviour
{
    private AnimationController animationController;

    private float attackCooldownCounter = 0;
    
    private Attack nextAttack = Attack.first;

    private void Start()
    {
        animationController = GetComponent<AnimationController>();
    }

    public void OnFire(float attackCooldown, float nextAttackTimeLimit)
    {
        if (attackCooldownCounter >= attackCooldown)
        {

            if(nextAttack == Attack.second && attackCooldownCounter - attackCooldown < nextAttackTimeLimit)
            {
                animationController.FireAnimation(Attack.second);
                nextAttack = Attack.first;
            }
            else
            {
                animationController.FireAnimation(Attack.first);
                nextAttack = Attack.second;
            }

            //Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(character.attackPoint.position, character.attackRange);

            //foreach (Collider2D character in hitCharacters)
            //{
            //    //TODO: damage
            //}

            attackCooldownCounter = 0;
        }
    }

    private void Update()
    {
        attackCooldownCounter += Time.deltaTime;
    }
}

using UnityEngine;
using static AnimationController;

[RequireComponent(typeof(AnimationController))]
public class AttackController : MonoBehaviour
{
    private AnimationController animationController;
    private Character character;

    private float attackCooldownCounter = 0;
    
    private Attack nextAttack = Attack.first;

    private void Start()
    {
        animationController = GetComponent<AnimationController>();
        character = GetComponent<Character>();
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

            Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(character.GetAttackPoint().position, character.GetAttackRange(), character.GetLayerMask());

            foreach (Collider2D potentialEnemy in hitCharacters)
            {
                Character enemyCharacter = potentialEnemy.GetComponent<Character>();
                if (!enemyCharacter.Equals(character))
                {
                    enemyCharacter.healthController.TakeDamage(character.GetWeaponDamage());
                }
            }

            attackCooldownCounter = 0;
        }
    }

    private void Update()
    {
        attackCooldownCounter += Time.deltaTime;
    }
}

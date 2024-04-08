using System.Collections;
using UnityEngine;
using static AnimationController;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent (typeof(Weapon))]
public class AttackController : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .5f;
    ///<summary> 
    ///Сколько времени дается игроку, чтобы совершить следующую атаку в серии 
    ///</summary>*/
    [SerializeField] private float nextAttackTimeLimit = .2f;

    private Character character;
    private AnimationController animationController;
    private Weapon weapon;

    private bool isAttacking = false;
    private bool canUseSecondAttack = true;

    private Attack nextAttack = Attack.first;

    private void Start()
    {
        character = GetComponent<Character>();
        animationController = GetComponent<AnimationController>();
        weapon = GetComponent<Weapon>();
    }

    public void OnFire()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            if (nextAttack == Attack.second && canUseSecondAttack)
            {
                animationController.FireAnimation(Attack.second);
                nextAttack = Attack.first;
            }
            else
            {
                animationController.FireAnimation(Attack.first);
                nextAttack = Attack.second;
            }
        }
    }

    //Вызывается из анимации, начало нанесения урона
    public void OnAttack()
    {
        Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, character.CharacterLayerMask);

        foreach (Collider2D potentialEnemy in hitCharacters)
        {
            Character enemyCharacter = potentialEnemy.GetComponent<Character>();
            if (!enemyCharacter.Equals(character))
            {
                enemyCharacter.healthController.TakeDamage(weapon.WeaponDamage);
            }
        }
    }

    //Вызывается в конце анимации
    public void OnAttackEnd(Attack attack)
    {
        canUseSecondAttack = true;
        if(attack == Attack.first)
        {
            StartCoroutine(nameof(EndOfTimeForSecondAttack));
        }
        isAttacking = false;
    }

    //Возможно в апдейте будет точнее измеряться время
    IEnumerator EndOfTimeForSecondAttack()
    {
        yield return new WaitForSeconds(nextAttackTimeLimit);
        canUseSecondAttack = false;
    }

    //DEBUG
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

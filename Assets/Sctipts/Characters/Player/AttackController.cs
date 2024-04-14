using System.Collections;
using UnityEngine;

public enum Attack
{
    first,
    second
}

[RequireComponent(typeof(Player))]
[RequireComponent (typeof(Weapon))]
public class AttackController : MonoBehaviour
{
    ///<summary> 
    ///Сколько времени дается игроку, чтобы совершить следующую атаку в серии 
    ///</summary>
    [SerializeField] private float nextAttackTimeLimit = .2f;

    private float allowMovingDelay = .1f;

    private Player player;
    private Weapon weapon;

    private Attack nextAttack = Attack.first;
    private bool canUseSecondAttack = true;

    private void Start()
    {
        player = GetComponent<Player>();
        weapon = GetComponent<Weapon>();
    }

    public void OnFire()
    {
        if (!player.isAttacking && !player.isRolling)
        {
            player.isAttacking = true;
            player.movementController.StopMoving();
            
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Debug.Log(difference);
            player.viewController.Turn(difference);

            if (nextAttack == Attack.second && canUseSecondAttack)
            {
                player.animationController.FireAnimation(Attack.second);
                nextAttack = Attack.first;
            }
            else
            {
                player.animationController.FireAnimation(Attack.first);
                nextAttack = Attack.second;
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

        player.isAttacking = false;
        //Продолжаем движение после совершенной атаки
        StartCoroutine(nameof(AllowMovingDelay));
    }

    IEnumerator EndOfTimeForSecondAttack()
    {
        yield return new WaitForSeconds(nextAttackTimeLimit);
        canUseSecondAttack = false;
    }

    //Задержка ради того, чтобы можно было совершить атаку 2
    IEnumerator AllowMovingDelay()
    {
        yield return new WaitForSeconds(allowMovingDelay);
        player.movementController.OnMove(player.moveLastContext);
    }
}

using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class AttackController : MonoBehaviour
{
    private AnimationController animationController;

    private float attackCooldownCounter = 0;
    public bool isAttacking { get; private set; } = false;

    private void Start()
    {
        animationController = GetComponent<AnimationController>();
    }

    public void OnFire(float attackCooldown)
    {
        //TODO: урон не повтор€ютс€
        if (!isAttacking && attackCooldownCounter >= attackCooldown)
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
}

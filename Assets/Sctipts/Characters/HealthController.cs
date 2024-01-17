using UnityEngine;

[RequireComponent(typeof(AnimationController))]
[RequireComponent (typeof(Character))]
public class HealthController : MonoBehaviour
{
    private AnimationController animationController;
    private Character character;
    
    private float currentHealth;

    private void Start()
    {
        animationController = GetComponent<AnimationController>();
        character = GetComponent<Character>();
        
        currentHealth = character.GetMaxHealth();
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                animationController.HurtAnimation();
            }
        }
    }

    private void Die()
    {
        animationController.DeathAnimation();

        // Disable collision and logics
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}

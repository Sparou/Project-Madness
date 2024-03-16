using UnityEngine;

[RequireComponent(typeof(AnimationController))]
[RequireComponent (typeof(Character))]
public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    private AnimationController animationController;
    private float currentHealth;

    public bool invincible = false;

    private void Start()
    {
        animationController = GetComponent<AnimationController>();
        
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!invincible && damage > 0)
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

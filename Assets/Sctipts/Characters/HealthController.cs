using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
[RequireComponent (typeof(Character))]
public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    private AnimationController animationController;
    public float currentHealth;

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

    public void TakeHeal(float heal)
    {
        if (currentHealth + heal > maxHealth) currentHealth = maxHealth;
        else currentHealth += heal;
    }

    private void Die()
    {
        animationController.DeathAnimation();

        //Disable collision and logics
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;

        var childs = gameObject.GetComponents<MonoBehaviour>();
        var collider = gameObject.GetComponent<Collider2D>();

        foreach (var child in childs)
        {
            child.enabled = false;
        }

        collider.enabled = false;
        //StartCoroutine(nameof(DisableAnimator));
        //Destroy(gameObject, animationController.animator.GetCurrentAnimatorStateInfo(0).length - 0.2f);
    }

    //IEnumerator DisableAnimator()
    //{
    //    yield return new WaitForSeconds(animationController.animator.GetCurrentAnimatorStateInfo(0).length - 0.1f);
    //    animationController.animator.enabled = false;
    //}
}

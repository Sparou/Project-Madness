using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AnimationController))]
[RequireComponent (typeof(Character))]
public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    private AnimationController animationController;
    public float currentHealth;
    public TMP_Text HealthText; 

    public bool invincible = false;

    private void Start()
    {
        animationController = GetComponent<AnimationController>();
        
        currentHealth = 10;
        UpdateHealth();
    }

    public void TakeDamage(float damage)
    {
        if (!invincible && damage > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            UpdateHealth();

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
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
        UpdateHealth();
    }

    private void Die()
    {
        animationController.DeathAnimation();

        //Disable collision and logics
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;

        var childs = gameObject.GetComponents<MonoBehaviour>();
        foreach (var child in childs)
        {
            child.enabled = false;
        }

        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Character>().enabled = false;


        //StartCoroutine(nameof(DisableAnimator));
        //Destroy(gameObject, animationController.animator.GetCurrentAnimatorStateInfo(0).length - 0.2f);
    }

    private void UpdateHealth()
    {
        HealthText.text = $"HP: {currentHealth}";
    }

    //IEnumerator DisableAnimator()
    //{
    //    yield return new WaitForSeconds(animationController.animator.GetCurrentAnimatorStateInfo(0).length - 0.1f);
    //    animationController.animator.enabled = false;
    //}
}

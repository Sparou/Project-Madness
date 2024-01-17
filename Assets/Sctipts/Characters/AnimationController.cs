using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator;

    public enum Attack
    {
        first,
        second
    }

    #region Animator variables names
    [SerializeField] private string animatorAttackTrigger = "AttackTrigger";
    [SerializeField] private string animatorSecondAttackTrigger = "SecondAttackTrigger";
    [SerializeField] private string animatorHurtTrigger = "HurtTrigger";
    [SerializeField] private string animatorDeathTrigger = "DeathTrigger";
    [SerializeField] private string animatorSpeed = "Speed";
    #endregion 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FireAnimation(Attack attack)
    {
        if (attack == Attack.first && !animator.GetBool(animatorAttackTrigger))
        {
            animator.ResetTrigger(animatorSecondAttackTrigger);
            animator.SetTrigger(animatorAttackTrigger);
        }
        else if (attack == Attack.second && !animator.GetBool(animatorSecondAttackTrigger))
        {
            animator.SetTrigger(animatorSecondAttackTrigger);
        }
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat(animatorSpeed, speed);
    }

    public void HurtAnimation()
    {
        if (!animator.GetBool(animatorHurtTrigger))
        {
            animator.SetTrigger(animatorHurtTrigger);
        }
    }

    public void DeathAnimation()
    {
        if (!animator.GetBool(animatorDeathTrigger))
        {
            animator.SetTrigger(animatorDeathTrigger);
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    //TODO: ÄÎËÆÍÎ ÁÛÒÜ ÏÐÎÒÅÊÒÅÄ, ÄÎÑÒÀÒÜ ÐÓÊÈ ÈÇ ÆÎÏÛ!

    protected Animator animator;

    #region Animator variables names
    [SerializeField] private string animatorAttackTrigger = "AttackTrigger";
    [SerializeField] private string animatorDodgeTrigger = "DodgeTrigger";
    [SerializeField] private string animatorRollTrigger = "RollTrigger";
    [SerializeField] private string animatorSecondAttackTrigger = "SecondAttackTrigger";
    [SerializeField] private string animatorHurtTrigger = "HurtTrigger";
    [SerializeField] private string animatorDeathTrigger = "DeathTrigger";
    [SerializeField] private string animatorSpeed = "Speed";
    [SerializeField] private string firstAttackSpeedMultiplier = "FirstAttackSpeedMultiplier";
    [SerializeField] private string secondAttackSpeedMultiplier = "SecondAttackSpeedMultiplier";
    #endregion 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FireAnimation(Attack attack)
    {
        animator.SetTrigger(attack == Attack.first ? animatorAttackTrigger : animatorSecondAttackTrigger);
        /*Êîä íà êîãäà áóäåò ìíîãî àòàê*/
        //if (attack == Attack.first && !animator.GetBool(animatorAttackTrigger))
        //{
        //    animator.SetTrigger(animatorAttackTrigger);
        //}
        //else if (attack == Attack.second && !animator.GetBool(animatorSecondAttackTrigger))
        //{
        //    animator.SetTrigger(animatorSecondAttackTrigger);
        //}
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

    public void DodgeAnimation()
    {
        if (!animator.GetBool(animatorDodgeTrigger))
        {
            animator.SetTrigger(animatorDodgeTrigger);
        }
    }

    public void RollAnimation()
    {
        if (!animator.GetBool(animatorRollTrigger))
        {
            animator.SetTrigger(animatorRollTrigger);
        }
    }
}

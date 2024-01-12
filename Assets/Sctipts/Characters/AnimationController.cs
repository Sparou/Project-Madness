using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMoveStart()
    {
        if (!animator.GetBool("MoveTrigger"))
        {
            animator.ResetTrigger("IdleTrigger");
            animator.SetTrigger("MoveTrigger");
        }
    }

    public void OnMoveEnd()
    {
        animator.ResetTrigger("MoveTrigger");
        animator.SetTrigger("IdleTrigger");
    }

    public void FireAnimation()
    {
        if (!animator.GetBool("AttackTrigger")) 
        {
            animator.SetTrigger("AttackTrigger");
        }
    }
}

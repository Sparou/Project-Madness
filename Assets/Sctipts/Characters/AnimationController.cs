using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        animator.ResetTrigger("IdleTrigger");
        animator.SetTrigger("MoveTrigger");
    }

    public void OnMoveEnd()
    {
        animator.ResetTrigger("MoveTrigger");
        animator.SetTrigger("IdleTrigger");
    }

    public void FireAnimation()
    {
        animator.SetTrigger("AttackTrigger");
    }



    // Update is called once per frame
    void Update()
    {

    }
}

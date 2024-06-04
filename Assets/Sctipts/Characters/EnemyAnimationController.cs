using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationController : AnimationController
{
    void Start()
    {
    }

    private void Update()
    {
       Debug.Log(animator.transform.name);
    }
}

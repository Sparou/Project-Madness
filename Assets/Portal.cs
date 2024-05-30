using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Ссылка на Animator
    public Animator animator;
    // Срабатывает при входе другого Collider в триггер
   private void OnTriggerEnter2D(Collider2D other)
    {
        // Активируем анимацию
        animator.SetBool("Animation", true);
    }
}

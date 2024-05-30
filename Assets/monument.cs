using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour
{
    // Ссылка на Animator
    public Animator animator;

    // Вероятность активации анимации
    public float animationProbability = 0.1f; // 10%

    // Срабатывает при входе другого Collider в триггер
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Генерируем случайное число между 0 и 1 используя UnityEngine.Random
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        // Если случайное число меньше или равно 0.1 (10%), активируем анимацию
        if (randomValue <= animationProbability)
        {
            // Включаем анимацию
            animator.SetBool("Animation", true);

            // Запускаем Coroutine, чтобы вернуть Animation в false после завершения анимации
            StartCoroutine(ResetAnimation());
        }
    }

    // Coroutine для сброса анимации после завершения
    private IEnumerator ResetAnimation()
    {
        // Получаем информацию о текущем анимационном состоянии
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Ждем завершения анимации
        yield return new WaitForSeconds(stateInfo.length);

        // Сбрасываем флаг, чтобы вернуть Animation в false
        animator.SetBool("Animation", false);
    }
}
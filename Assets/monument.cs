using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour
{
    // ������ �� Animator
    public Animator animator;

    // ����������� ��������� ��������
    public float animationProbability = 0.1f; // 10%

    // ����������� ��� ����� ������� Collider � �������
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������� ��������� ����� ����� 0 � 1 ��������� UnityEngine.Random
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        // ���� ��������� ����� ������ ��� ����� 0.1 (10%), ���������� ��������
        if (randomValue <= animationProbability)
        {
            // �������� ��������
            animator.SetBool("Animation", true);

            // ��������� Coroutine, ����� ������� Animation � false ����� ���������� ��������
            StartCoroutine(ResetAnimation());
        }
    }

    // Coroutine ��� ������ �������� ����� ����������
    private IEnumerator ResetAnimation()
    {
        // �������� ���������� � ������� ������������ ���������
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // ���� ���������� ��������
        yield return new WaitForSeconds(stateInfo.length);

        // ���������� ����, ����� ������� Animation � false
        animator.SetBool("Animation", false);
    }
}
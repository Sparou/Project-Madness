using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // ������ �� Animator
    public Animator animator;
    // ����������� ��� ����� ������� Collider � �������
   private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������� ��������
        animator.SetBool("Animation", true);
    }
}

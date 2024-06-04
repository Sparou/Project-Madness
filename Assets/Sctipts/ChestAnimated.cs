using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimated : MonoBehaviour
{
    private Animator animator2;
    private void Awake()
    {
        animator2 = GetComponent<Animator>();
    }
    public void OpenChest()
    {
        animator2.SetBool("Open", true);
    }
    public void CloseChest()
    {
        animator2.SetBool("Open", false);
    }
}

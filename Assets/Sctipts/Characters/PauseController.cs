using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    protected bool dialogueIsPlaying = false;

    protected void Update()
    {
        GameObject managers = GameObject.Find("Managers");
        if (managers)
        {
            Transform dialogueManager = managers.transform.Find("DialogueManager");
            if (dialogueManager)
            {
                DialogueManager comp = dialogueManager.GetComponent<DialogueManager>();
                if (comp)
                {
                    dialogueIsPlaying = comp.dialogueIsPlaying;
                }
                else
                {
                    dialogueIsPlaying = false;
                }
            }
        }
        else
        {
            dialogueIsPlaying = false;
        }

        var childs = gameObject.GetComponents<MonoBehaviour>();
        foreach (var child in childs)
        {
            if (child != this)
            {
                child.enabled = !dialogueIsPlaying;
            }
        }
    }
}

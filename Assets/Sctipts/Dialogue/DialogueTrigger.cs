using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;
    DialogueManager dialogueManager;

    private bool isTriggered;
    private NPC currentNpc;

    private void Awake()
    {
        dialogueManager = DialogueManager.instance;
        isTriggered = false;
    }

    private void Start()
    {
        currentNpc = GetComponent<NPC>();
    }

    private void Update()
    {
        if (isTriggered && !dialogueManager.dialogueIsPlaying)
        {
            
            dialogueManager.EnterDialogue(_inkJSON,currentNpc.characterName);
            isTriggered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTriggered = true;
        }    
    }

}

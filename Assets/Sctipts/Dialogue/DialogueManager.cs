using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI actorName;
    [SerializeField] private TextMeshProUGUI messegeText;


    public bool dialogueIsPlaying { get; private set; }
    private Story currentStory;

    

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }
    

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        // TODO: New input system support
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogue(TextAsset inkJSON, String NPCname)
    {
        Debug.Log("Entered");
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        currentStory = new Story(inkJSON.text);

        actorName.text = NPCname;

        ContinueStory();

    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            messegeText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }
}

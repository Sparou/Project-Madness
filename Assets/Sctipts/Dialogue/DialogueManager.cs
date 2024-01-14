using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI actorName;
    [SerializeField] private TextMeshProUGUI messegeText;
    [SerializeField] private GameObject[] choices;

    public bool dialogueIsPlaying { get; private set; }
    private Story currentStory;
    private TextMeshProUGUI[] textChoices;
    private DialogueVariables dialogueVariables;




    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            dialogueVariables = new DialogueVariables();
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        textChoices = new TextMeshProUGUI[choices.Length];

        int i = 0;
        foreach (GameObject choice in choices) 
        {
            textChoices[i] = choice.GetComponentInChildren<TextMeshProUGUI>();
            i++;
        }
    }

    private void OnSubmit()
    {
        Debug.Log("Entered!");
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        // TODO: New input system support
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ContinueStory();
        }
    }

    public void EnterDialogue(TextAsset inkJSON, string NPCname)
    {
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        currentStory = new Story(inkJSON.text);

        dialogueVariables.StartListening(currentStory);

        actorName.text = NPCname;

        ContinueStory();

    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            messegeText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogue();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        int i = 0;

        // Displays choices in UI holders
        foreach (Choice choice in currentChoices)
        {
            choices[i].gameObject.SetActive(true);
            textChoices[i].text = choice.text;
            i++;
        }

        // Hides unused choice holders
        int bound = currentChoices.Count > 0 ? currentChoices.Count : choices.Length;
        for (int j = i; j < bound; j++)
        {
            choices[j].gameObject.SetActive(false);
        }
    }

    private void ExitDialogue()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Calculate total RP points
        dialogueVariables.StopListening(currentStory);
    }

    private IEnumerator SelectChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (choices.Length > 0)
        {
            Debug.Log(choiceIndex);
            currentStory.ChooseChoiceIndex(choiceIndex);
        }
    }


}

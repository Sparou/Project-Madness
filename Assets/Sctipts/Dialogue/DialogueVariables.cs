using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DialogueVariables : MonoBehaviour
{
    private string inkPointsParam = "humanPoints";
    private Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
    private Dictionary<string, Ink.Runtime.Object> variables;

    public DialogueVariables(TextAsset globalsReadableJSON) 
    {

        // convert compiled JSON with globals to story object 
        Story globalsReadableStory = new Story(globalsReadableJSON.text); 


        // copy global variable state from Ink file to dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalsReadableStory.variablesState)
        {
            Ink.Runtime.Object value = globalsReadableStory.variablesState.GetVariableWithName(name);
            variables[name] = value;
        }

    }

    public void StartListening(Story story)
    {
        // load saved variables to current story
        GlobalsToStory(story);

        story.variablesState.variableChangedEvent += VariableChanged;

    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;     
    }

    public bool CheckDialogue(string name)
    {
        return variables.ContainsKey(name) && ((Ink.Runtime.IntValue)variables[name]).value < 1;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        
        if (variables.ContainsKey(name))
        {
            variables[name] = value;

            // change human points of player in main script
            if (name == inkPointsParam && player != null)
            {
                player.ChangePoints(((Ink.Runtime.IntValue) value).value);
            }
        }

    }

    private void GlobalsToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}

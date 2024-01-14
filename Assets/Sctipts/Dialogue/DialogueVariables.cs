using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueVariables : MonoBehaviour
{
    private string inkPointsParam = "totalPoints";
    private Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

    public void StartListening(Story story)
    {

    }

    public void StopListening(Story story)
    {
        if (player != null && story.variablesState[inkPointsParam] != null)
        {
            player._humanPoints += (int)story.variablesState[inkPointsParam];
        }     
    }
}

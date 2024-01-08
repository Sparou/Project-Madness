using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField] private ChestAnimated chest;
    [SerializeField] Transform Player;
    private float distance;
    private void Update()
    {
        distance = Vector3.Distance(transform.position, Player.position);
        if (Input.GetKeyDown(KeyCode.F) && distance < 2)
        {
            chest.OpenChest();
        }
        if (Input.GetKeyDown(KeyCode.G) && distance < 2)
        {
            chest.CloseChest();
        }

    }
}

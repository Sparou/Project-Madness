using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private DoorAnimated door;
    [SerializeField] Transform Player;

    private BoxCollider2D myCollider;
    private float distance;
    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        

    }
    private void Update()
    {
            distance = Vector3.Distance(transform.position, Player.position);
            if (Input.GetKeyDown(KeyCode.F) && distance < 2)
            {
                door.OpenDoor();
                myCollider.enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.G) && distance < 2)
            {
                door.CloseDoor();
                myCollider.enabled = true;
            }

    }
}

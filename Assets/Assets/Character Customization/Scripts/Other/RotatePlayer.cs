
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    // ~~ 1. Handles animation updates to rotate player

    private Animator animator;
    private Vector2[] positions;
    private int currentPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        CreatePositions();
    }

    private void CreatePositions()
    {
        positions = new Vector2[2];
        positions[0] = new Vector2(-1, 0);      // Left
        positions[1] = new Vector2(1, 0);       // Right
    }

    public void RotateLeft()
    {
        if (currentPosition < positions.Length - 1)
        {
            currentPosition++;
        }
        else
        {
            currentPosition = 0;
        }

        UpdatePosition();
    }

    public void RotateRight()
    {
        if (currentPosition > 0)
        {
            currentPosition--;
        }
        else
        {
            currentPosition = positions.Length - 1;
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        animator.SetFloat("moveX", positions[currentPosition].x);
        //animator.SetFloat("moveY", positions[currentPosition].y);
    }
}

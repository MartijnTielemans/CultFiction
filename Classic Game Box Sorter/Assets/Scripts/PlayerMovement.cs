using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    GameManager manager;

    public InputAction move;

    [SerializeField] Vector2 currentPosition;
    [SerializeField] Vector2 newPosition;

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input from the move input
        Vector2 input = move.ReadValue<Vector2>();

        int rowPositions = manager.rows[GetRow(currentPosition.y)].Count;

        // If input left or right, up or down
        switch (input.x)
        {
            case -1:
                // Can't go left if on the furthest point
                if (currentPosition.x != 0)
                {
                    newPosition.x--;
                }
                break;

            case 1:
                // Can't go right if on the furthest point
                if (currentPosition.x != rowPositions)
                {
                    newPosition.x++;
                }
                break;

            case 0 when input.y == -1:
                if (currentPosition.y != 0)
                {
                    newPosition.y++;
                }
                break;

            case 0 when input.y == 1:
                if (currentPosition.y != manager.rows[GetRow(currentPosition.y)].Count)
                {
                    newPosition.y--;
                }
                break;

            default:
                break;
        }

        ChangePosition(newPosition);
    }

    // Translates the player's position
    void ChangePosition(Vector2 newPos)
    {
        // Get the row the player is on
        List<GameObject> currentRow = manager.rows[GetRow(newPos.y)];

        // Set position according to current row and position
        transform.position = manager.GetPosition(currentRow, (int)newPos.x).transform.position;

        // Set current position
        currentPosition = newPos;
    }

    int GetRow(float pos)
    {
        for (int i = 0; i < manager.rows.Count; i++)
        {
            if (pos == i)
            {
                return i;
            }
        }

        return 0;
    }
}

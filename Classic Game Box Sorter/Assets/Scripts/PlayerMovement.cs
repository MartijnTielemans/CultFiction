using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    GameManager manager;
    GridManager gridManager;

    public InputAction move;

    GameObject[,] grid;

    [SerializeField] Vector2 currentPosition;
    [SerializeField] Vector2 newPosition;

    [SerializeField] bool holdingPackage;

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
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        grid = gridManager.GetNodes();

        // Set the starting position to the middle of the top row
        currentPosition = new Vector2(grid.GetLength(1) / 2, 0);
        newPosition = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input from the move input
        Vector2 input = move.ReadValue<Vector2>();

        // If input left or right, up or down
        // Can only move up or down in the middle of the map
        switch (input.x)
        {
            case -1:
                // Check if position is null
                if (grid[(int)currentPosition.x, (int)currentPosition.y - 1] != null)
                {
                    newPosition.y--;
                }
                break;

            case 1:
                if (grid[(int)currentPosition.x, (int)currentPosition.y + 1] != null)
                {
                    newPosition.y++;
                }
                break;

            case 0 when input.y == -1:
                if (currentPosition.x == grid.GetLength(1) / 2)
                {
                    if (grid[(int)currentPosition.x - 1, (int)currentPosition.y] != null)
                    {
                        newPosition.x--;
                    }
                }
                break;

            case 0 when input.y == 1:
                if (currentPosition.x == grid.GetLength(1) / 2)
                {
                    if (grid[(int)currentPosition.x + 1, (int)currentPosition.y] != null)
                    {
                        newPosition.x++;
                    }
                }
                break;

            default:
                break;
        }

        // Change the player position based on newPosition if its different from currentPosition
        if (newPosition != currentPosition)
            ChangePosition(newPosition);
    }

    // Translates the player's position
    void ChangePosition(Vector2 newPos)
    {
        transform.position = grid[(int)newPos.x, (int)newPos.y].transform.position;

        // Set current position
        currentPosition = newPos;
    }

    void DeliverPacket()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] positions;
    List<GameObject> row;
    public List<List<GameObject>> rows;

    [SerializeField] int rowAmount = 2;

    private void Awake()
    {
        gameObject.tag = "GameManager";

        for (int i = 0; i < rowAmount; i++)
        {
            // add a row to the rows List
            //rows.Add(new row);
        }

        // Put the positions inside their respective rows
        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < positions.Length; j++)
            {
                Position pos = positions[j].GetComponent<Position>();

                // If the position's row is the same as the current row
                if (pos.row == i)
                {
                    // Add position to that row
                    rows[i].Add(positions[j]);
                }
            }
        }
    }

    public GameObject GetPosition(List<GameObject> row, int i)
    {
        return row[i];
    }
}

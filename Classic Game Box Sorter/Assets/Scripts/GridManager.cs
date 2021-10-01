using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject node;

    GameObject[,] nodes;
    [SerializeField] int xSize, ySize;
    [SerializeField] float rowSpacing, colSpacing;

    private void Awake()
    {
        gameObject.tag = "GridManager";
    }

    private void Start()
    {
        nodes = new GameObject[ySize, xSize];

        for (int c = 0; c < ySize; c++)
        {
            for (int r = 0; r < xSize; r++)
            {
                if (HasNode(c, r))
                {
                    GameObject n = Instantiate(node, MakeNodePosition(r, c, rowSpacing, colSpacing), Quaternion.identity);
                    nodes[c, r] = n;
                }
                else
                {
                    nodes[c, r] = null;
                }
            }
        }
    }

    bool HasNode(int cols, int rows)
    {
        // Check if node map has a node
        return true;
    }

    Vector2 MakeNodePosition(float currentRow, float currentCol, float rSpacing, float cSpacing)
    {
        return new Vector2(currentRow * -rSpacing, currentCol * cSpacing);
    }

    public GameObject[,] GetNodes()
    {
        return nodes;
    }
}
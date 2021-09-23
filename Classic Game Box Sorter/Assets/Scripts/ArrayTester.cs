using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayTester : MonoBehaviour
{
    public Array2D<int> MyArray = new Array2D<int>(4, 4);

    // Start is called before the first frame update
    void Start()
    {
        for (int r = 0; r < MyArray.GetLength(0); r++)
        {
            for (int c = 0; c < MyArray.GetLength(1); c++)
            {
                Debug.Log($"Item ({r},{c}) = {MyArray[r, c]}");
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

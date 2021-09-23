using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Array2D<T> : ISerializationCallbackReceiver
{
    T[,] array;
    [SerializeField] T[] storage;
    [SerializeField] int rows; 
    [SerializeField] int cols;

    public T this[int r, int c]
    {
        get
        {
            return array[r, c];
        }
        set
        {
            array[r, c] = value;
        }
    }

    public T[,] Raw => array;

    public int GetLength(int dimension)
    {
        return array.GetLength(dimension);
    }

    
        
    public Array2D(int rows, int cols) {
        this.rows = rows;
        this.cols = cols;
        this.array = new T[rows, cols];
        this.storage = new T[rows * cols];
    }

    Vector2Int Index1DTo2D(int index)
    {
        Vector2Int result = new Vector2Int(index/rows, index%rows);
        return result;
    }

    int Index2DTo1D(int x, int y)
    {
        int result = x*rows+y;
        return result;
    }

    void UpdateSizes()
    {
        if(array.GetLength(0) != rows || array.GetLength(1) != cols)
        {
            T[,] temp = new T[rows, cols];
            for (int r = 0; r < array.GetLength(0); r++)
            {
                for (int c = 0; c < array.GetLength(1); c++)
                {
                    if(r < rows && c < cols)
                    {
                        temp[r, c] = array[r, c];
                    }
                }
            }
            array = temp;
        }

        //TODO: Protect against changes that keep the total size the same. (ex: remove row and add col) MAYBE???
        if(storage.Length != rows * cols)
        {
            Array.Resize<T>(ref storage, rows * cols);
        }
    }

    public void OnBeforeSerialize()
    {
        UpdateSizes();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                storage[Index2DTo1D(r, c)] = array[r,c];
            }
        }
    }

    public void OnAfterDeserialize()
    {
        UpdateSizes();
        for (int i = 0; i < storage.Length; i++)
        {
            Vector2Int indexes = Index1DTo2D(i);
            array[indexes.x, indexes.y] = storage[i];
        }
    }


}

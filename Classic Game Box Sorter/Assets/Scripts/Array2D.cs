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
 
    public int Cols
    {
        get
        {
            if (array != null)
            {
                return array.GetLength(0);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            if (value != cols || array == null)
            {
                T[,] temp = new T[value, rows];
                for (int c = 0; c < Cols; c++)
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        if (r < rows && c < cols)
                        {
                            temp[c, r] = array[c, r];
                        }
                    }
                }
                array = new T[value, rows];
                Array.Copy(temp, array, rows*cols);
                cols = value;

                if (storage != null)
                {
                    Array.Resize<T>(ref storage, rows * cols);
                }
                else
                {

                    storage = new T[rows * cols];
                }
            }
        }
    }

    public int Rows
    {
        get
        {
            if(array != null)
            {
                return array.GetLength(1);
            } else
            {
                return 0;
            }
            
        }
        set
        {
            if (value != rows || array == null)
            {
                T[,] temp = new T[cols, value];
                
                for (int c = 0; c < Cols; c++)
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        if (r < rows && c < cols)
                        {
                            temp[c, r] = array[c, r];
                        }
                    }
                }
                array = new T[cols, value];
                Array.Copy(temp, array, rows * cols);
                rows = value;
                if(storage != null)
                {
                    Array.Resize<T>(ref storage, rows * cols);
                }
                else
                {

                    storage = new T[rows * cols];
                }
                
            }
        }
    }

    public T this[int c, int r]
    {
        get
        {
            return array[c, r];
        }
        set
        {
            array[c, r] = value;
        }
    }

    public T[,] Raw => array;

    public int GetLength(int dimension)
    {
        return array.GetLength(dimension);
    }
        
    public Array2D(int cols, int rows)
    {
        this.Rows = rows;
        this.Cols = cols;
        //this.array = new T[cols, rows];
    }

    Vector2Int Index1DTo2D(int index)
    {
        Vector2Int result = new Vector2Int(index/rows, index%rows);
        return result;
    }

    int Index2DTo1D(int x, int y)
    {
        int result = y*cols+x;
        return result;
    }

    public void OnBeforeSerialize()
    {
        Debug.Log("Entered OnBeforeSerialize");
        if (array != null)
        {
            //storage = new T[cols * rows];
            for (int c = 0; c < array.GetLength(0); c++)
            {
                for (int r = 0; r < array.GetLength(1); r++)
                {
                    try
                    {
                        storage[Index2DTo1D(c, r)] = array[c, r];
                    }
                    catch
                    {
                        Debug.LogError($"Trying to access ({c},{r}) but array has size ({array.GetLength(0)},{array.GetLength(1)})");
                    }
                }
            }
        }
        Debug.Log("Exited OnBeforeSerialize");

    }

    public void OnAfterDeserialize()
    {
        Debug.Log("Entered OnAfterDeserialize");
        //Debug.Log($"After deserialize size is {rows * cols} and storage length is {storage.Length}");
        //Debug.Log($"AND rows is {rows} and cols is {cols}");

        array = new T[cols,rows]; 

        for (int i = 0; i < storage.Length; i++)
        {
            Vector2Int indexes = Index1DTo2D(i);
            //Debug.Log($"index {i} translated to coordinate ({indexes.x},{indexes.y})"); 
            array[indexes.x, indexes.y] = storage[i];
        }
        Debug.Log("Exited OnAfterDeserialize");
    }
}

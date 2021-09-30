using System;

[Serializable]
public class LevelSave
{
    public string levelName;
    public bool[] map;
    public int width;
    public int height;

    public LevelSave(string levelName, bool[,] map)
    {
        this.levelName = levelName;
        this.map = Convert2DArray(map);
        this.width = map.GetLength(0);
        this.height = map.GetLength(1);
    }

    bool[] Convert2DArray(bool[,] map)
    {
        bool[] result = new bool[map.GetLength(0) * map.GetLength(1)];
        int i = 0;

        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                result[i++] = map[x, y];
            }
        }

        return result;
    }

    public bool[,] Convert1DArray(bool[] map)
    {
        bool[,] temp = new bool[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                temp[x, y] = map[y * width + x];
            }
        }

        return temp;
    }
}
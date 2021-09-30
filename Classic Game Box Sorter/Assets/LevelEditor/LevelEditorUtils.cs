using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelEditorUtils
{
    public static string directory = Application.dataPath + "/LevelEditor/Resources/";
    public static string extension = ".json";

    public static void Save(string name, bool[,] map)
    {
        // Save JSON to Resources folder
        // Check if the directory exists
        if (!Directory.Exists(directory))
        {
            // if directory does not exist, error message
            Debug.LogError($"Could not find directory: {directory}");
        }
        else
        {
            // Create a new LevelSave
            LevelSave save = new LevelSave(name, map);

            // Pass the new LevelSave object to a JSON format
            string json = JsonUtility.ToJson(save);

            File.WriteAllText(directory + name + extension, json);
        }
    }

    public static bool [,] Load(string name)
    {
        // Load JSON from resources folder
        // Make filePath
        string filePath = directory + name + extension;

        // Check if the file exists
        if (!Exists(name))
        {
            Debug.LogError($"Could not find file: {name}");
        }
        else
        {
            string json = File.ReadAllText(filePath);
            LevelSave load = JsonUtility.FromJson<LevelSave>(json);

            return load.Convert1DArray(load.map);
        }

        return null;
    }

    public static bool Exists(string name)
    {
        // Check if file exists in Resources folder.
        return File.Exists(directory + name + extension);
    }
}

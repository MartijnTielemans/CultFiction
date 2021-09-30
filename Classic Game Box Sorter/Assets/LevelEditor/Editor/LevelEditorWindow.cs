using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditorWindow : EditorWindow
{
    private string levelName;
    private bool editting = false;
    private int xSize;
    private int ySize;
    private bool[,] map;
    private Vector2 _scrollPosition;

    [MenuItem("Window/Level Editor")]
    public static void ShowWindow()
    {
        LevelEditorWindow window = EditorWindow.GetWindow(typeof(LevelEditorWindow)) as LevelEditorWindow;
        window.titleContent = new GUIContent("Level editor");
    }

    private void OnEnable()
    {
        
    }

    private void OnGUI()
    {
        if(!editting)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name:");
            levelName = GUILayout.TextField(levelName);
            GUILayout.EndHorizontal();

            GUILayout.Space(20);
            if (GUILayout.Button("Edit"))
            {
                // Name cannot be empty or whitespace
                if (string.IsNullOrWhiteSpace(levelName))
                {
                    Debug.LogError("Level Name is empty");
                }
                else
                {
                    // Call to load function
                    // Check if the filename exists
                    if (LevelEditorUtils.Exists(levelName))
                    {
                        // Call Load function
                        Debug.Log("Load File");
                        map = LevelEditorUtils.Load(levelName);
                        xSize = map.GetLength(0);
                        ySize = map.GetLength(1);
                    }

                    editting = true;
                }
            }
        }    
        else
        {
            GUILayout.Label($"Editting level {levelName}");
            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label("X Size");
            xSize = EditorGUILayout.IntField(xSize);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Y Size");
            ySize = EditorGUILayout.IntField(ySize);
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Space(10);

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            GUILayout.BeginVertical();
            for (int y = 0; y < ySize; y++)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < xSize; x++)
                {
                    if(map != null && x < map.GetLength(0) && y < map.GetLength(1))
                    {
                        //TODO:??
                    }
                    else
                    {
                        if(map == null)
                        {
                            map = new bool[xSize, ySize];
                        }
                        else
                        {
                            //bool[,] temp = new bool[xSize, ySize];
                            //...
                            map = new bool[xSize, ySize];
                        }
                    }
                    GUILayout.MinWidth(20);
                    map[x, y] = GUILayout.Toggle(map[x, y], "");
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.Space(30);

            if (GUILayout.Button("Save Level"))
            {
                // Call Save function
                Debug.Log("Save called");
                LevelEditorUtils.Save(levelName, map);
            }

            if (GUILayout.Button("Cancel"))
            {
                editting = false;
            }
        }
        
    }
}

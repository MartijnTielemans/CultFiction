using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public GameObject node;

    GameObject[,] nodes;
    public Array2D<bool> nodeMap = new Array2D<bool>(2, 5);
    [SerializeField] int rows, cols;
    [SerializeField] float rowSpacing, colSpacing;

    private void Awake()
    {
        gameObject.tag = "GameManager";
    }

    private void Start()
    {
        nodes = new GameObject[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (HasNode(rows, cols))
                {
                    Debug.Log("1");
                    GameObject n = Instantiate(node, MakeNodePosition(r, c, rowSpacing, colSpacing), Quaternion.identity);
                    nodes[rows, cols] = n;
                }
                else
                {
                    Debug.Log("2");
                    nodes[rows, cols] = null;
                }
            }
        }
    }

    bool HasNode(int rows, int cols)
    {
        return nodeMap[rows, cols];
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

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager)), CanEditMultipleObjects]
public class GameManagerEditor : Editor
{
    private SerializedProperty rows, cols;
    private SerializedProperty rowSpacing, colSpacing;
    private SerializedProperty node;


    void OnEnable()
    {
        rows = serializedObject.FindProperty("rows");
        cols = serializedObject.FindProperty("cols");
        rowSpacing = serializedObject.FindProperty("rowSpacing");
        colSpacing = serializedObject.FindProperty("colSpacing");
        node = serializedObject.FindProperty("node");
    }

    public override void OnInspectorGUI()
    {
        GameManager obj = (GameManager)target;
        serializedObject.Update();
        EditorGUILayout.PropertyField(node);
        EditorGUILayout.PropertyField(rows);
        EditorGUILayout.PropertyField(cols);
        EditorGUILayout.PropertyField(rowSpacing);
        EditorGUILayout.PropertyField(colSpacing);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Node map");

        if (obj.nodeMap == null || obj.nodeMap.GetLength(0) != rows.intValue || obj.nodeMap.GetLength(1) != cols.intValue)
        {
            obj.nodeMap = new Array2D<bool>(rows.intValue, cols.intValue);

            for (int r = 0; r < rows.intValue; r++)
            {
                for (int c = 0; c < cols.intValue; c++)
                {
                    obj.nodeMap[r, c] = true;
                }
            }
        }

        GUILayout.BeginVertical(GUI.skin.box);

        for (int r = 0; r < rows.intValue; r++)
        {
            GUILayout.BeginHorizontal();
            for (int c = 0; c < cols.intValue; c++)
            {
                obj.nodeMap[r, c] = GUILayout.Toggle(obj.nodeMap[r, c], "");
            }
            GUILayout.EndHorizontal();
        }
        
        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

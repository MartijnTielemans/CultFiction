using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridManager : MonoBehaviour
{
    public GameObject node;

    GameObject[,] nodes;
    public Array2D<bool> nodeMap;
    [SerializeField] int rows, cols;
    [SerializeField] float rowSpacing, colSpacing;

    private void Awake()
    {
        gameObject.tag = "GridManager";
    }

    private void Start()
    {
        nodes = new GameObject[cols, rows];

        for (int c = 0; c < cols; c++)
        {
            for (int r = 0; r < rows; r++)
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
        return nodeMap[cols, rows];
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
[CustomEditor(typeof(GridManager)), CanEditMultipleObjects]
public class GridManagerEditor : Editor
{
    private SerializedProperty rows, cols;
    private SerializedProperty rowSpacing, colSpacing;
    private SerializedProperty node;
    private SerializedProperty nodeMap;


    void OnEnable()
    {
        rows = serializedObject.FindProperty("rows");
        cols = serializedObject.FindProperty("cols");
        rowSpacing = serializedObject.FindProperty("rowSpacing");
        colSpacing = serializedObject.FindProperty("colSpacing");
        node = serializedObject.FindProperty("node");
        nodeMap = serializedObject.FindProperty("nodeMap");
    }

    public override void OnInspectorGUI()
    {
        GridManager obj = (GridManager)target;
        serializedObject.Update();
        EditorGUILayout.PropertyField(node);
        EditorGUILayout.PropertyField(rows);
        EditorGUILayout.PropertyField(cols);
        EditorGUILayout.PropertyField(rowSpacing);
        EditorGUILayout.PropertyField(colSpacing);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Node map");

        //if (obj.nodeMap == null || obj.nodeMap.GetLength(0) != rows.intValue || obj.nodeMap.GetLength(1) != cols.intValue)
        //{
        //    obj.nodeMap = new Array2D<bool>(rows.intValue, cols.intValue);

        //    for (int c = 0; c < cols.intValue; c++)
        //    {
        //        for (int r = 0; r < rows.intValue; r++)
        //        {
        //            obj.nodeMap[c, r] = true;
        //        }
        //    }
        //}
        EditorGUILayout.PropertyField(nodeMap);
        serializedObject.ApplyModifiedProperties();
        GUILayout.BeginVertical(GUI.skin.box);

        //obj.nodeMap.Cols = cols.intValue;
        //obj.nodeMap.Rows = rows.intValue;

        //for (int c = 0; c < cols.intValue; c++)
        //{
        //    GUILayout.BeginHorizontal();
        //    for (int r = 0; r < rows.intValue; r++)
        //    {
        //        obj.nodeMap[c, r] = GUILayout.Toggle(obj.nodeMap[c, r], "");
        //    }
        //    GUILayout.EndHorizontal();
        //}
        
        GUILayout.EndVertical();

        

        
    }
}
#endif

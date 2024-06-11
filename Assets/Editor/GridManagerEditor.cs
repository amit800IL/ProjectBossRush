using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridManager gridManager = (GridManager)target;

        if (!EditorApplication.isPlaying)
        {
            EditorGUILayout.HelpBox("Press Clear Grid before pressing play after using editor grid generation", MessageType.Info);

            if (GUILayout.Button("Generate Grid"))
            {
                gridManager.CreateGrid();
            }

            if (GUILayout.Button("Clear Grid"))
            {
                gridManager.ClearGrid();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Grid controls are disabled during runtime.", MessageType.Info);
        }
    }
}

#endif

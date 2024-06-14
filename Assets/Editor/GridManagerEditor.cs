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
            if (GUILayout.Button("Generate Grid"))
            {
                gridManager.CreateGrid();
            }

            if (GUILayout.Button("Clear Grid"))
            {
                gridManager.ClearGrid();
            }
        }
    }
}

#endif

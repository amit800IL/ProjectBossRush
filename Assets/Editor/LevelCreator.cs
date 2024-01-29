using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelCreator))]
public class LevelCreator : Editor
{
    private SerializedProperty gridSize;
    private SerializedProperty tilePrefab;

    private void OnEnable()
    {
        gridSize = serializedObject.FindProperty("gridSize");
        tilePrefab = serializedObject.FindProperty("tilePrefab");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(gridSize);
        EditorGUILayout.PropertyField(tilePrefab);

        if (GUILayout.Button("Create Grid"))
        {
            GridManager levelCreator = (GridManager)target;
            levelCreator.CreateGrid();
        }

        serializedObject.ApplyModifiedProperties();
    }
}

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorInspector : Editor
{
    //private LevelEditor levelEditor;

    //private void OnEnable()
    //{
    //    levelEditor = target as LevelEditor;
    //}

    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update();

    //    EditorGUILayout.LabelField("Grid Objects", EditorStyles.boldLabel);

    //    if (GUILayout.Button("Add Object"))
    //    {
    //        levelEditor.AddObject();
    //    }

    //    SerializedProperty objects = serializedObject.FindProperty("levelObjects");

    //    for (int i = 0; i < objects.arraySize; i++)
    //    {
    //        SerializedProperty obj = objects.GetArrayElementAtIndex(i);

    //        EditorGUILayout.BeginHorizontal();

    //        EditorGUILayout.PropertyField(obj.FindPropertyRelative("prefab"), GUIContent.none);
    //        EditorGUILayout.PropertyField(obj.FindPropertyRelative("color"), GUIContent.none);

    //        EditorGUILayout.BeginVertical();

    //        EditorGUILayout.PropertyField(obj.FindPropertyRelative("tileType"), GUIContent.none);

    //        EditorGUILayout.BeginHorizontal();

    //        MoveObjectX(obj);
    //        MoveObjectY(obj);

    //        EditorGUILayout.EndHorizontal();
    //        EditorGUILayout.EndVertical();

    //        if (GUILayout.Button("Remove"))
    //        {
    //            levelEditor.RemoveObject(i);
    //        }

    //        EditorGUILayout.EndHorizontal();

    //        GUILayout.Space(10);
    //    }

    //    serializedObject.ApplyModifiedProperties();
    //}

    //private void OnSceneGUI()
    //{
    //    Event guiEvent = Event.current;

    //    if (guiEvent.type == EventType.Layout)
    //    {
    //        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
    //    }

    //    foreach (Tile obj in levelEditor.levelObjects)
    //    {
    //        Vector2 worldPosition = new Vector2(obj.Position.x, obj.Position.y);
    //        Vector2 handlePosition = Handles.PositionHandle(worldPosition, Quaternion.identity);

    //        if (worldPosition != handlePosition)
    //        {
    //            Undo.RecordObject(levelEditor, "Move Level Object");

    //            Vector2Int tilePosition = new Vector2Int(Mathf.RoundToInt(handlePosition.x), Mathf.RoundToInt(handlePosition.y));

    //            obj.SetTilePosition(tilePosition);

    //            SceneView.RepaintAll();
    //        }
    //    }
    //}

    //private void MoveObjectX(SerializedProperty obj)
    //{
    //    EditorGUILayout.LabelField("X:" + obj.FindPropertyRelative("position.x").intValue);

    //    if (GUILayout.Button("+"))
    //    { 
    //        obj.FindPropertyRelative("position.x").intValue += 1;
    //    }
    //    if (GUILayout.Button("-"))
    //    {
    //        obj.FindPropertyRelative("position.x").intValue -= 1;
    //    }

    //    EditorGUILayout.EndHorizontal();
    //}

    //private void MoveObjectY(SerializedProperty obj)
    //{
    //    EditorGUILayout.LabelField("Y: " + obj.FindPropertyRelative("position.y").intValue);

    //    EditorGUILayout.BeginVertical(GUILayout.Width(10));

    //    if (GUILayout.Button("+"))
    //    {
    //        obj.FindPropertyRelative("position.y").intValue += 1;
    //    }
    //    if (GUILayout.Button("-"))
    //    {
    //        obj.FindPropertyRelative("position.y").intValue -= 1;
    //    }
    //}
}
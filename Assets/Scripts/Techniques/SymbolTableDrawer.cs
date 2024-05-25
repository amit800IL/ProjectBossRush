
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(SymbolTable))]
public class SymbolTableDrawer : PropertyDrawer
{
    private static readonly string[] symbolNames = System.Enum.GetNames(typeof(SymbolTable.Symbols));

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty tableProperty = property.FindPropertyRelative("table");

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        EditorGUI.indentLevel++;

        float labelWidth = 70f; 
        float fieldWidth = 50f; 

        for (int i = 0; i < SymbolTable.SYMBOL_TYPE_COUNT; i++)
        {
            Rect labelRect = new Rect(position.x - 120, position.y + (i * EditorGUIUtility.singleLineHeight), labelWidth, EditorGUIUtility.singleLineHeight);
            Rect fieldRect = new Rect((position.x + labelWidth) - 100, position.y + (i * EditorGUIUtility.singleLineHeight), fieldWidth, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(labelRect, new GUIContent(symbolNames[i]));
            EditorGUI.PropertyField(fieldRect, tableProperty.GetArrayElementAtIndex(i), GUIContent.none);
        }

        EditorGUI.indentLevel--;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return SymbolTable.SYMBOL_TYPE_COUNT * EditorGUIUtility.singleLineHeight;
    }
}

#endif
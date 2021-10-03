using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelEditorManager))]
public class LevelEditorManagerEditor : Editor
{
    SerializedProperty width;
    SerializedProperty height;

    private void OnEnable()
    {
        width = serializedObject.FindProperty("width");
        height = serializedObject.FindProperty("height");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        LevelEditorManager lvEditor = (LevelEditorManager)target;

        EditorGUILayout.HelpBox("Width, Height 값을 입력하고 SetSize를 누르면 사이즈가 변합니다.\n내용은 저장되지 않습니다.", MessageType.Warning);

        serializedObject.Update();

        EditorGUILayout.LabelField("Canvas");
        GUILayout.BeginHorizontal();
        {
            EditorGUILayout.PropertyField(width);
            EditorGUILayout.PropertyField(height);
        }
        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("SetSize"))
        {
            lvEditor.ResetCanvas(lvEditor.width, lvEditor.height);
        }

        GUILayout.EndHorizontal();
    }
}

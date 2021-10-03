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

        EditorGUILayout.HelpBox("Width, Height ���� �Է��ϰ� SetSize�� ������ ����� ���մϴ�.\n������ ������� �ʽ��ϴ�.", MessageType.Warning);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JsonIO))]
public class JsonIOEditor : Editor
{
    bool updateFileFoldout = false;

    SerializedProperty stage;
    SerializedProperty fileName;

    SerializedProperty updateStage;
    SerializedProperty updatefileName;
    SerializedProperty fromCode;
    SerializedProperty toCode;

    private void OnEnable()
    {
        stage = serializedObject.FindProperty("stage");
        fileName = serializedObject.FindProperty("fileName");

        updateStage = serializedObject.FindProperty("updateStage");
        updatefileName = serializedObject.FindProperty("updatefileName");
        fromCode = serializedObject.FindProperty("fromCode");
        toCode = serializedObject.FindProperty("toCode");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("stage : ����� ����\nfile name : ���ϸ�\n���ϸ��� \'level_*\'�� ��Ģ�� �����ּ���.\n����� �ε尡 ������ �̷������� �ܼ�â���� Ȯ�����ּ���.", MessageType.Info);
        //base.OnInspectorGUI();

        JsonIO jsonIO = (JsonIO)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(stage);
        EditorGUILayout.PropertyField(fileName);

        serializedObject.ApplyModifiedProperties();

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("SaveToJson"))
        {
            jsonIO.SaveToJson();
        }

        if (GUILayout.Button("LoadJson"))
        {
            jsonIO.LoadJson();
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        updateFileFoldout = EditorGUILayout.Foldout(updateFileFoldout, "updateCode");
        {
            if (updateFileFoldout)
            {
                serializedObject.Update();

                EditorGUI.indentLevel += 2;

                EditorGUILayout.PropertyField(updateStage);
                EditorGUILayout.PropertyField(updatefileName);
                EditorGUILayout.PropertyField(fromCode);
                EditorGUILayout.PropertyField(toCode);

                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("Update"))
                {
                    jsonIO.UpdateTileCode();
                }

                EditorGUI.indentLevel -= 2;
            }
        }
    }
}

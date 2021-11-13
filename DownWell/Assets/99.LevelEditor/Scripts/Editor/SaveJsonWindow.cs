using UnityEngine;
using UnityEditor;

public class SaveJsonWindow : EditorWindow
{
    private SerializedProperty stage;
    SerializedProperty fileName;

    int width = 11;
    int height = 11;

    private JsonIO jsonIO;

    public static void ConversationSystem(JsonIO target)
    {
        var window = GetWindow<SaveJsonWindow>();
        window.minSize = new Vector2(500, 136);
        window.maxSize = new Vector2(500, 136);

        window.jsonIO = target;
    }

    private void OnGUI()
    {
        EditorGUILayout.HelpBox("stage : ����� ����\nfile name : ���ϸ�\n���ϸ��� \'level_*\'�� ��Ģ�� �����ּ���.\n����� �ε尡 ������ �̷������� �ܼ�â���� Ȯ�����ּ���.", MessageType.Info);

        var serializedObject = new SerializedObject(jsonIO);

        stage = serializedObject.FindProperty("stage");
        fileName = serializedObject.FindProperty("fileName");

        serializedObject.Update();

        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(stage);
        EditorGUILayout.PropertyField(fileName);

        GUILayout.BeginHorizontal();
        {
            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Save"))
        {
            var newLevel = jsonIO.CreateLevelJson(width, height);
            jsonIO.LoadJson(newLevel.path);
            jsonIO.SelectDB(newLevel);

            this.Close();
        }
    }
}

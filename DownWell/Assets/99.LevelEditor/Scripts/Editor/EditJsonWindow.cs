using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditJsonWindow : EditorWindow
{
    //private SerializedProperty stage;
    //SerializedProperty fileName;

    private LevelEditor.Stage stage;
    private string directory;
    private string fileName;

    int width = 11;
    int height = 11;

    private JsonIO jsonIO;
    private LevelDBInfo editLevelDB;

    public static void ConversationSystem(JsonIO target, LevelDBInfo editLevelDB)
    {
        var window = GetWindow<EditJsonWindow>(true);
        window.minSize = new Vector2(500, 130-20);
        window.maxSize = new Vector2(500, 130-20);

        window.jsonIO = target;
        window.editLevelDB = editLevelDB;
        window.stage = editLevelDB.stage;
        window.fileName = editLevelDB.filename;
        window.directory = editLevelDB.path.Replace(editLevelDB.filename + ".json", "");
    }

    private void OnGUI()
    {
        EditorGUILayout.HelpBox("directory : ����� ����\nfile name : ���ϸ�\n����� �ε尡 ������ �̷������� �ܼ�â���� Ȯ�����ּ���.", MessageType.Info);

        var serializedObject = new SerializedObject(jsonIO);

        //stage = serializedObject.FindProperty("stage");
        //fileName = serializedObject.FindProperty("fileName");

        //serializedObject.Update();

        EditorGUILayout.BeginVertical();
        {
            //EditorGUILayout.BeginHorizontal();
            //{
            //    EditorGUILayout.PrefixLabel("Stage");
            //    stage = (LevelEditor.Stage)EditorGUILayout.EnumPopup(stage);
            //}
            //EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PrefixLabel("Directory");
                directory = EditorGUILayout.TextField(directory);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PrefixLabel("File Name");
                fileName = EditorGUILayout.TextField(fileName);
            }
            EditorGUILayout.EndHorizontal();

            //EditorGUILayout.PropertyField(stage);
            //EditorGUILayout.PropertyField(fileName);

            //GUILayout.BeginHorizontal();
            //{
            //    width = EditorGUILayout.IntField("Width", width);
            //    height = EditorGUILayout.IntField("Height", height);
            //}
            //GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        //serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Edit"))
        {
            //jsonIO.Edit(editLevelDB, fileName, stage, editLevelDB == jsonIO.selectedLevelDB);
            jsonIO.Edit(editLevelDB, fileName, directory, editLevelDB == jsonIO.selectedLevelDB);

            //// Delete Database, Json;
            //var backUpLevel = jsonIO.DeleteJson(editLevelDB);

            //// Save Database, Json
            //jsonIO.Save(backUpLevel, fileName, stage);

            this.Close();
        }
    }
}

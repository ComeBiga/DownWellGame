using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditJsonWindow : EditorWindow
{
    //private SerializedProperty stage;
    //SerializedProperty fileName;

    private LevelEditor.Stage stage;
    private string fileName;

    int width = 11;
    int height = 11;

    private JsonIO jsonIO;
    private LevelDBInfo editLevelDB;

    public static void ConversationSystem(JsonIO target, LevelDBInfo editLevelDB)
    {
        var window = GetWindow<EditJsonWindow>();
        window.minSize = new Vector2(500, 136);
        window.maxSize = new Vector2(500, 136);

        window.jsonIO = target;
        window.editLevelDB = editLevelDB;
    }

    private void OnGUI()
    {
        EditorGUILayout.HelpBox("stage : 저장될 폴더\nfile name : 파일명\n파일명은 \'level_*\'의 규칙을 따라주세요.\n저장과 로드가 적절히 이뤄졌는지 콘솔창에서 확인해주세요.", MessageType.Info);

        var serializedObject = new SerializedObject(jsonIO);

        //stage = serializedObject.FindProperty("stage");
        //fileName = serializedObject.FindProperty("fileName");

        //serializedObject.Update();

        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PrefixLabel("Stage");
                stage = (LevelEditor.Stage)EditorGUILayout.EnumPopup(stage);
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
            jsonIO.Edit(editLevelDB, fileName, stage, editLevelDB == jsonIO.selectedLevelDB);

            //// Delete Database, Json;
            //var backUpLevel = jsonIO.DeleteJson(editLevelDB);

            //// Save Database, Json
            //jsonIO.Save(backUpLevel, fileName, stage);

            this.Close();
        }
    }
}

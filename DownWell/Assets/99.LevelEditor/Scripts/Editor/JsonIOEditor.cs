using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JsonIO))]
public class JsonIOEditor : Editor
{
    JsonIO jsonIO;

    bool updateFileFoldout = false;

    SerializedProperty stage;
    SerializedProperty fileName;

    SerializedProperty updateStage;
    SerializedProperty updatefileName;
    SerializedProperty fromCode;
    SerializedProperty toCode;

    string searchName = "search...";

    Vector2 scrollPos;

    private void OnEnable()
    {
        jsonIO = (JsonIO)target;

        stage = serializedObject.FindProperty("stage");
        fileName = serializedObject.FindProperty("fileName");

        updateStage = serializedObject.FindProperty("updateStage");
        updatefileName = serializedObject.FindProperty("updatefileName");
        fromCode = serializedObject.FindProperty("fromCode");
        toCode = serializedObject.FindProperty("toCode");

        searchName = "";
    }

    public override void OnInspectorGUI()
    {
        //EditorGUILayout.HelpBox("stage : 저장될 폴더\nfile name : 파일명\n파일명은 \'level_*\'의 규칙을 따라주세요.\n저장과 로드가 적절히 이뤄졌는지 콘솔창에서 확인해주세요.", MessageType.Info);
        //base.OnInspectorGUI();

        //JsonIO jsonIO = (JsonIO)target;

        //serializedObject.Update();

        //EditorGUILayout.PropertyField(stage);
        //EditorGUILayout.PropertyField(fileName);

        //serializedObject.ApplyModifiedProperties();

        //GUILayout.BeginHorizontal();

        //if(GUILayout.Button("SaveToJson"))
        //{
        //    jsonIO.SaveToJson();
        //}

        //if (GUILayout.Button("LoadJson"))
        //{
        //    jsonIO.LoadJson();
        //}

        //GUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal("HelpBox");

        EditorGUILayout.LabelField(jsonIO.selectedLevelDB.filename, EditorStyles.boldLabel);

        EditorGUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Create New Level"))
        {
            //EditorWindow.GetWindow(typeof(SaveJsonWindow), true);
            //var saveJsonWindow = EditorWindow.GetWindow<SaveJsonWindow>(true);
            if (!EditorApplication.isPlaying)
            {
                EditorUtility.DisplayDialog("", "플레이 모드에서 이용해주세요!", "네");
            }
            else 
            { 
                SaveJsonWindow.ConversationSystem((JsonIO)target);
            }
        }

        //if (GUILayout.Button("LoadToJson"))
        //{
        //    EditorWindow.GetWindow(typeof(LoadJsonWindow), true);
        //    //var saveJsonWindow = EditorWindow.GetWindow<SaveJsonWindow>(true);
        //    //SaveJsonWindow.ConversationSystem((JsonIO)target);
        //}

        GUILayout.EndHorizontal();

        //SearchBar();
        DisplayLevelList();

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

    void SearchBar()
    {
        searchName = EditorGUILayout.TextField(searchName, (GUIStyle)"SearchTextField");
        var searchList = jsonIO.levelDB.jsonLevelDBs.FindAll(lv => lv.filename.ToLower().Contains(searchName.ToLower()));
    }

    void DisplayLevelList()
    {
        EditorGUILayout.BeginVertical("Helpbox");

        searchName = EditorGUILayout.TextField(searchName, (GUIStyle)"SearchTextField");
        var searchList = jsonIO.levelDB.jsonLevelDBs.FindAll(lv => lv.filename.ToLower().Contains(searchName.ToLower()));

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(100));

        foreach (var db in searchList)
        {
            DrawJsonRow(db);
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.EndVertical();
    }

    void DrawJsonRow(LevelDBInfo db)
    {
        EditorGUILayout.BeginHorizontal();

        var buttonLabel = db.filename;

        if (JsonIO.levelChanged)
            buttonLabel += " *";

        if (GUILayout.Button(buttonLabel, GUILayout.ExpandWidth(true)))
        {
            if (!EditorApplication.isPlaying)
            {
                EditorUtility.DisplayDialog("", "플레이 모드에서 이용해주세요!", "네");
            }
            else
            {
                jsonIO.SelectDB(db);
                jsonIO.LoadJson(db.path);
            }
        }

        if (GUILayout.Button("E", GUILayout.Width(20)))
        {
            if (!EditorApplication.isPlaying)
            {
                EditorUtility.DisplayDialog("", "플레이 모드에서 이용해주세요!", "네");
            }
            else
            {
                //jsonIO.DeleteJson(db);
                EditJsonWindow.ConversationSystem((JsonIO)target, db);
            }
        }

        if (GUILayout.Button("X", GUILayout.Width(20)))
        {
            if (!EditorApplication.isPlaying)
            {
                EditorUtility.DisplayDialog("", "플레이 모드에서 이용해주세요!", "네");
            }
            else
            {
                //jsonIO.DeleteJson(db);
                DeleteLevelWindow.ConversationSystem((JsonIO)target, db);
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    public static void CheckAndEnterPlayMode()
    {
        if(!EditorApplication.isPlaying)
        {
            EditorApplication.EnterPlaymode();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StageEditor : EditorBase
{
    CatDown.StageDatabase stageDB;

    Vector2 scrollPos;

    CatDown.StageInfo selectedStage;

    public StageEditor()
    {
        //stageDB = CatDown.Maker.StageManager.instance.database;
    }

    public void SetDatabase(CatDown.StageDatabase stageDB)
    {
        this.stageDB = stageDB;
    }

    public override void Draw()
    {
        EditorGUILayout.BeginHorizontal();
        

        EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(200), GUILayout.Height(400));
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            //searchName = EditorGUILayout.TextField(searchName, (GUIStyle)"SearchTextField");

            EditorGUILayout.LabelField("Stage List", EditorStyles.boldLabel, GUILayout.Width(180));

            if(GUILayout.Button("Create"))
            {
                stageDB.AddStage();
            }

            DisplayStageList();

            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();

        DrawDetail();

        EditorGUILayout.EndHorizontal();
    }

    void DisplayStageList()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        foreach(var stage in stageDB.stages)
        {
            DisplayStageListRow(stage);
        }

        EditorGUILayout.EndVertical();
    }

    void DisplayStageListRow(CatDown.StageInfo stage)
    {
        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button(stage.name))
        {
            selectedStage = stage;
        }

        if(GUILayout.Button("X", GUILayout.Width(20)))
        {
            stageDB.DeleteStage(stage);
        }

        EditorGUILayout.EndHorizontal();
    }

    void DrawDetail()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(200), GUILayout.Height(400));

        EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);



        EditorGUILayout.EndVertical();

        EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);



        EditorGUILayout.EndVertical();

        EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);



        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }
}

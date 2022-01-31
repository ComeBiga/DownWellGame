using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StageEditor : EditorBase
{
    public static string stageDBPath = "Assets/9.Database/Stage/StageDB.asset";
    CatDown.StageDatabase stageDB;

    string searchName = "";

    Vector2 scrollPos;

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

        }

        if(GUILayout.Button("X", GUILayout.Width(20)))
        {
            stageDB.DeleteStage(stage);
        }

        EditorGUILayout.EndHorizontal();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StageEditor : EditorBase
{
    static StageList stageList;

    string searchName = "";

    Vector2 scrollPos;

    public StageEditor(StageList stageList)
    {
        StageEditor.stageList = stageList;
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
                StageEditor.stageList.AddStage();
            }

            DisplayStageList();

            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();
    }

    void DisplayStageList()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        foreach(var stage in StageEditor.stageList.stages)
        {
            DisplayStageListRow(stage);
        }

        EditorGUILayout.EndVertical();
    }

    void DisplayStageListRow(StageDatabase stage)
    {
        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button(stage.name))
        {

        }

        if(GUILayout.Button("X", GUILayout.Width(20)))
        {
            StageEditor.stageList.DeleteStage(stage);
        }

        EditorGUILayout.EndHorizontal();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JsonIO))]
public class JsonIOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        JsonIO jsonIO = (JsonIO)target;

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
    }
}

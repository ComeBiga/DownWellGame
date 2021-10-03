using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JsonIO))]
public class JsonIOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("stage : 저장될 폴더\nfile name : 파일명\n파일명은 \'level_*\'의 규칙을 따라주세요.\n저장과 로드가 적절히 이뤄졌는지 콘솔창에서 확인해주세요.", MessageType.Info);
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

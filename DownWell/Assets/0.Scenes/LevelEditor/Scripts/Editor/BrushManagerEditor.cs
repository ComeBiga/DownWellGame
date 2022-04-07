using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BrushManager))]
public class BrushManagerEditor : Editor
{
    BrushManager brushManager;

    SerializedProperty stage;
    SerializedProperty wallBrushes;
    SerializedProperty enemyBrushes;
    SerializedProperty eraserBrush;

    int enemyCode = 0;
    bool changeColorFoldout = false;

    Vector2 scrollPos;
    int brushType = 0;

    int selInt = 0;

    private void OnEnable()
    {
        brushManager = (BrushManager)target;

        stage = serializedObject.FindProperty("stage");
        wallBrushes = serializedObject.FindProperty("wallBrushes");
        enemyBrushes = serializedObject.FindProperty("enemyBrushes");
        eraserBrush = serializedObject.FindProperty("eraserBrush");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        EditorGUILayout.HelpBox("각 버튼을 누르고 그리면 그려집니다. \n다른 개체를 그리려면 아래에 소스를 추가하면 됩니다.", MessageType.Info);
        EditorGUILayout.LabelField("Brush", EditorStyles.boldLabel);

        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PropertyField(stage, new GUIContent("DB"));
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("HelpBox", GUILayout.ExpandWidth(false), GUILayout.Width(300));
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            brushType = GUILayout.Toolbar(brushType, new string[] { "Wall", "Enemy", "ERASER" });

            EditorGUILayout.EndHorizontal();

            switch(brushType)
            {
                case 0:
                    //CreateBrushButton();
                    DisplayWallBrushButton();
                    //brushManager.ChangeBrush(brushManager.brushDB.wallBrushes[0]);
                    break;
                case 1:
                    //CreateBrushButton();
                    DisplayEnemyBrushButton();
                    //brushManager.ChangeBrush(brushManager.brushDB.enemyBrushes[0]);
                    break;
                case 2:
                    brushManager.ChangeToEraser();
                    break;
            }
            
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.LabelField("Current Brush", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical("HelpBox");
        {
            EditorGUILayout.LabelField(brushManager.currentBrush.name, EditorStyles.boldLabel);
        }
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();

        //EditorGUILayout.Space();
        //changeColorFoldout = EditorGUILayout.Foldout(changeColorFoldout, "Brush Sources");
        //{
        //    if (changeColorFoldout)
        //    {
        //        serializedObject.Update();

        //        EditorGUI.indentLevel += 2;
        //        EditorGUILayout.PropertyField(wallBrushes);
        //        EditorGUILayout.PropertyField(enemyBrushes);
        //        EditorGUILayout.PropertyField(eraserBrush);
        //        EditorGUI.indentLevel -= 2;

        //        serializedObject.ApplyModifiedProperties();
        //    }
        //}
        //base.OnInspectorGUI();
    }

    //void CreateBrushButton()
    //{
    //    if (GUILayout.Button("New Brush"))
    //    {
    //        switch (brushType)
    //        {
    //            case 0:
    //                brushManager.brushDB.wallBrushes.Add(brushManager.eraserBrush);
    //                break;
    //            case 1:
    //                brushManager.brushDB.enemyBrushes.Add(brushManager.eraserBrush);
    //                break;
    //        }
    //    }
    //}

    #region DisplayBrush

    //void DisplayWallBrushRow()
    //{
    //    GUILayout.BeginHorizontal("HelpBox");
    //    {
    //        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(120));

    //        GUILayout.BeginHorizontal();

    //        for (int i = 0, hCount = 0; i < brushManager.brushDB.wallBrushes.Count; i++, hCount++)
    //        {
    //            //if(hCount >= 5)
    //            //{
    //            //    GUILayout.EndHorizontal();
    //            //    hCount = 0;
    //            //    GUILayout.BeginHorizontal();
    //            //}
    //            GUILayout.BeginHorizontal();

    //            LevelObject brush = brushManager.brushDB.wallBrushes[i];
    //            //LevelObject brush = brushManager.GetWallObjects()[i];

    //            if (GUILayout.Button(new GUIContent(brush.GetTexture2D()), GUILayout.Width(50), GUILayout.Height(50)))
    //            {
    //                brushManager.ChangeBrush(brush);
    //            }


    //            if (GUILayout.Button(brush.name, GUILayout.Width(100)))
    //            {
    //                brushManager.ChangeBrush(brush);
    //            }

    //            brushManager.brushDB.wallBrushes[i] = EditorGUILayout.ObjectField(brush, typeof(WallInfo), false) as LevelObject;

    //            if (GUILayout.Button("X", GUILayout.Width(20)))
    //            {
    //                brushManager.brushDB.wallBrushes.Remove(brush);
    //            }

    //            GUILayout.EndHorizontal();
    //        }

    //        GUILayout.EndHorizontal();

    //        EditorGUILayout.EndScrollView();
    //    }
    //    GUILayout.EndHorizontal();
    //}

    void DisplayWallBrushButton()
    {
        GUILayout.BeginHorizontal("HelpBox");
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(120));

            GUILayout.BeginHorizontal();
            {
                for (int i = 0, hCount = 0; i < brushManager.GetWallObjects().Count; i++, hCount++)
                {
                    if (hCount >= 5)
                    {
                        GUILayout.EndHorizontal();
                        hCount = 0;
                        GUILayout.BeginHorizontal();
                    }

                    LevelObject brush = brushManager.GetWallObjects()[i];

                    if (GUILayout.Button(new GUIContent(brush.GetTexture2D()), GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        brushManager.ChangeBrush(brush);
                    }
                }
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }
        GUILayout.EndHorizontal();
    }

    //void DisplayEnemyBrushRow()
    //{
    //    GUILayout.BeginHorizontal("HelpBox");
    //    {
    //        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(100));

    //        for (int i = 0; i < brushManager.brushDB.enemyBrushes.Count; i++)
    //        {
    //            GUILayout.BeginHorizontal();

    //            LevelObject brush = brushManager.brushDB.enemyBrushes[i];

    //            if (GUILayout.Button(brush.name, GUILayout.Width(100)))
    //            {
    //                brushManager.ChangeBrush(brush);
    //            }

    //            brushManager.brushDB.enemyBrushes[i] = EditorGUILayout.ObjectField(brush, typeof(EnemyInfo), false) as LevelObject;

    //            if (GUILayout.Button("X", GUILayout.Width(20)))
    //            {
    //                brushManager.brushDB.enemyBrushes.Remove(brush);
    //            }

    //            GUILayout.EndHorizontal();
    //        }

    //        EditorGUILayout.EndScrollView();
    //    }
    //    GUILayout.EndHorizontal();
    //}

    void DisplayEnemyBrushButton()
    {
        GUILayout.BeginHorizontal("HelpBox");
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(100));

            GUILayout.BeginHorizontal();
            {
                for (int i = 0, hCount = 0; i < brushManager.GetEnemyObjects().Count; i++, hCount++)
                {
                    if (hCount >= 5)
                    {
                        GUILayout.EndHorizontal();
                        hCount = 0;
                        GUILayout.BeginHorizontal();
                    }

                    LevelObject brush = brushManager.GetEnemyObjects()[i];

                    if (GUILayout.Button(new GUIContent(brush.GetTexture2D()), GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        brushManager.ChangeBrush(brush);
                    }
                }
            }
            GUILayout.EndHorizontal();
            
            EditorGUILayout.EndScrollView();
        }
        GUILayout.EndHorizontal();
    }

    #endregion

    #region Current Brush Info

    void DisplayCurrentBrushInfo()
    {

    }

    #endregion

    string[] DisplayEnemyBrushes(List<LevelObject> enemyBrushes)
    {
        string[] displayList = new string[enemyBrushes.ToArray().Length];

        for(int i = 0; i< enemyBrushes.ToArray().Length; i++)
        {
            displayList[i] = enemyBrushes[i].name;
        }

        return displayList;
    }
}

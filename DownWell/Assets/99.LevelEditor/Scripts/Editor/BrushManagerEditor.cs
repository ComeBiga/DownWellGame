using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BrushManager))]
public class BrushManagerEditor : Editor
{
    BrushManager brushManager;

    SerializedProperty wallBrushes;
    SerializedProperty enemyBrushes;
    SerializedProperty eraserBrush;

    int enemyCode = 0;
    bool changeColorFoldout = false;

    Vector2 scrollPos;
    bool brushType = false;

    private void OnEnable()
    {
        brushManager = (BrushManager)target;

        wallBrushes = serializedObject.FindProperty("wallBrushes");
        enemyBrushes = serializedObject.FindProperty("enemyBrushes");
        eraserBrush = serializedObject.FindProperty("eraserBrush");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        EditorGUILayout.HelpBox("각 버튼을 누르고 그리면 그려집니다. \n다른 개체를 그리려면 아래에 소스를 추가하면 됩니다.", MessageType.Info);
        EditorGUILayout.LabelField("Brush");
        EditorGUILayout.BeginVertical(GUI.skin.GetStyle("HelpBox"));
        {
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Wall"))
                {
                    brushManager.ChangeWallBrush(1);
                }

                if (GUILayout.Button("Block"))
                {
                    brushManager.ChangeWallBrush(2);
                }

                if (GUILayout.Button("Plat"))
                {
                    brushManager.ChangeWallBrush(3);
                }

                if (GUILayout.Button("Eraser"))
                {
                    brushManager.ChangeToEraser();
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                if (brushManager.currentBrush.code > 10)
                {
                    brushManager.ChangeEnemyBrush(enemyCode);
                }
                if (GUILayout.Button("Enemy"))
                {
                    brushManager.ChangeEnemyBrush(enemyCode);
                }

                enemyCode = EditorGUILayout.Popup(enemyCode, DisplayEnemyBrushes(brushManager.enemyBrushes));
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        //CreateBrushButton();
        //DisplayBrushRow();

        EditorGUILayout.Space();
        changeColorFoldout = EditorGUILayout.Foldout(changeColorFoldout, "Brush Sources");
        {
            if (changeColorFoldout)
            {
                serializedObject.Update();

                EditorGUI.indentLevel += 2;
                EditorGUILayout.PropertyField(wallBrushes);
                EditorGUILayout.PropertyField(enemyBrushes);
                EditorGUILayout.PropertyField(eraserBrush);
                EditorGUI.indentLevel -= 2;

                serializedObject.ApplyModifiedProperties();
            }
        }
        //base.OnInspectorGUI();
    }

    void CreateBrushButton()
    {
        if (GUILayout.Button("New Brush"))
        {
            brushManager.brushDB.brushes.Add(brushManager.eraserBrush);
        }
    }

    void DisplayBrushRow()
    {
        GUILayout.BeginHorizontal("HelpBox");
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(100));

            for (int i = 0; i < brushManager.brushDB.brushes.Count; i++)
            {
                GUILayout.BeginHorizontal();

                LevelObject brush = brushManager.brushDB.brushes[i];

                if (GUILayout.Button(brush.name, GUILayout.Width(100)))
                {
                    brushManager.ChangeBrush(brush);
                }

                brushManager.brushDB.brushes[i] = EditorGUILayout.ObjectField(brush, typeof(LevelObject), false) as LevelObject;

                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    brushManager.brushDB.brushes.Remove(brush);
                }

                GUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }
        GUILayout.EndHorizontal();
    }

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

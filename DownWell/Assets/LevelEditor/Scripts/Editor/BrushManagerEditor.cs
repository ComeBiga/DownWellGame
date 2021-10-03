using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BrushManager))]
public class BrushManagerEditor : Editor
{
    SerializedProperty wallBrushes;
    SerializedProperty enemyBrushes;
    SerializedProperty eraserBrush;

    int enemyCode = 0;
    bool changeColorFoldout = false;

    private void OnEnable()
    {
        wallBrushes = serializedObject.FindProperty("wallBrushes");
        enemyBrushes = serializedObject.FindProperty("enemyBrushes");
        eraserBrush = serializedObject.FindProperty("eraserBrush");
    }

    public override void OnInspectorGUI()
    {
        BrushManager brushManager = (BrushManager)target;

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
                    brushManager.ChangeEnemyBrush(enemyCode + 11);
                }
                if (GUILayout.Button("Enemy"))
                {
                    brushManager.ChangeEnemyBrush(enemyCode + 11);
                }

                enemyCode = EditorGUILayout.Popup(enemyCode, DisplayEnemyBrushes(brushManager.enemyBrushes));
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

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

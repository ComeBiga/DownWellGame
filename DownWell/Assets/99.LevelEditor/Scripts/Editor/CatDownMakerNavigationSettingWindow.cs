using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CatDownMakerNavigationSettingWindow : EditorWindow
{
    static float windowWidth = 200;
    static float windowHeight = 300;

    public static void OpenSettingWindow(CatDownMakerNavigationWindow nav)
    {
        var window = GetWindow<CatDownMakerNavigationSettingWindow>(true, "Setting");
        window.maxSize = new Vector2(windowWidth, windowHeight);
        window.minSize = new Vector2(windowWidth, windowHeight);
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        {

        }
        EditorGUILayout.EndVertical();
    }
}

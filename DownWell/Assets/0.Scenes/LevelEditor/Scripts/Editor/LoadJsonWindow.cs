
using UnityEngine;
using UnityEditor;

public class LoadJsonWindow : EditorWindow
{


    private void OnGUI()
    {
        EditorGUILayout.ObjectField("Label", new TextAsset(), typeof(TextAsset), false);
    }
}

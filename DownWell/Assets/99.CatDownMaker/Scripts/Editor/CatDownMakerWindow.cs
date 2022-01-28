using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CatDownMakerWindow : EditorWindow
{
    static float windowWidth;
    static float windowHeight;

    static StageEditor stageEditor;
    static List<EditorBase> editors = new List<EditorBase>();

    int editorSelected;

    [MenuItem("CatDownMaker", menuItem = "4DX/CatDownMaker")]
    public static void OpenCatDownMaker()
    {
        var window = GetWindow<CatDownMakerWindow>("CatDownMaker", true);
        //window.minSize = new Vector2(windowWidth, windowHeight);
        //window.maxSize = new Vector2(windowWidth, windowHeight);
    }

    private void OnEnable()
    {
        stageEditor = new StageEditor(StageList.GetStageList());

        editors.Clear();
        editors.Add(stageEditor);
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        {
            editorSelected = GUILayout.Toolbar(editorSelected, new string[] { "StageEditor" });

            editors[editorSelected].Draw();

            //DisplayDataBaseList();
        }
        EditorGUILayout.EndVertical();

    }

    void DisplayDataBaseList()
    {

    }
}

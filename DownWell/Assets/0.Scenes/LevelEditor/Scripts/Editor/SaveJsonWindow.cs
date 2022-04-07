using UnityEngine;
using UnityEditor;

public class SaveJsonWindow : EditorWindow
{
    private LevelEditor.Stage stage;
    private string directory;
    private string fileName;

    int width = 11;
    int height = 11;

    private JsonIO jsonIO;

    public static void ConversationSystem(JsonIO target)
    {
        var window = GetWindow<SaveJsonWindow>(true);
        window.minSize = new Vector2(500, 140-10);
        window.maxSize = new Vector2(500, 140-10);

        window.jsonIO = target;
        window.directory = "/Resources/Levels/";
        window.fileName = "";//AutoStageFileName(window.stage);
        //EditorGUI.FocusTextInControl("fileName");
    }

    private void OnGUI()
    {
        EditorGUILayout.HelpBox("Directory : 저장될 폴더\nfile name : 파일명\n저장과 로드가 적절히 이뤄졌는지 콘솔창에서 확인해주세요.", MessageType.Info);

        var serializedObject = new SerializedObject(jsonIO);

        //stage = serializedObject.FindProperty("stage");
        //fileName = serializedObject.FindProperty("fileName");

        serializedObject.Update();

        EditorGUILayout.BeginVertical();

        //EditorGUILayout.PropertyField(stage);
        //EditorGUILayout.PropertyField(fileName);

        //jsonIO.fileName = AutoStageFileName(jsonIO.stage);

        //EditorGUILayout.BeginHorizontal();
        //{
        //    EditorGUILayout.PrefixLabel("Stage");

        //    EditorGUI.BeginChangeCheck();
        //    {
        //        stage = (LevelEditor.Stage)EditorGUILayout.EnumPopup(stage);
        //    }
        //    if (EditorGUI.EndChangeCheck())
        //    {
        //        EditorGUI.FocusTextInControl("fileName");
        //        fileName = AutoStageFileName(stage);
        //    }
        //}
        //EditorGUILayout.EndHorizontal();
        
        // Folder Path
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PrefixLabel("Directory");

            directory = EditorGUILayout.TextField(directory);
        }
        EditorGUILayout.EndHorizontal();

        // File Name
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PrefixLabel("File Name");

            GUI.SetNextControlName("fileName");
            fileName = EditorGUILayout.TextField(fileName);
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Save"))
        {
            //JsonIOEditor.CheckAndEnterPlayMode();

            //var newLevel = jsonIO.CreateNewLevel(fileName, stage, width, height);
            var newLevel = jsonIO.CreateNewLevel(fileName, directory, width, height);
            jsonIO.LoadJson(newLevel.path);
            jsonIO.SelectDB(newLevel);

            this.Close();
        }
    }

    public static string AutoStageFileName(LevelEditor.Stage stage)
    {
        switch(stage)
        {
            case LevelEditor.Stage.Stage1:
                return "S1_";
            case LevelEditor.Stage.Stage2:
                return "S2_";
            case LevelEditor.Stage.Stage3:
                return "S3_";
            case LevelEditor.Stage.Stage4:
                return "S4_";
            case LevelEditor.Stage.Stage5:
                return "S5_";
            default:
                return "";

        }
    }
}

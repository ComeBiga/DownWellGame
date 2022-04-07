using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeleteLevelWindow : EditorWindow
{
    private JsonIO jsonIO;
    private LevelDBInfo deleteLevelDB;

    public static void ConversationSystem(JsonIO target, LevelDBInfo deleteLevelDB)
    {
        var window = GetWindow<DeleteLevelWindow>(true);
        window.titleContent = new GUIContent($"Delete \"{deleteLevelDB.filename}\"");
        window.minSize = new Vector2(500, 100);
        window.maxSize = new Vector2(500, 100);
        
        window.jsonIO = target;
        window.deleteLevelDB = deleteLevelDB;
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox($"\"{deleteLevelDB.filename}\"을(를) 삭제하시겠습니까?", MessageType.Warning);
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("Yes"))
            {
                jsonIO.DeleteJson(deleteLevelDB);

                this.Close();
            }

            if(GUILayout.Button("No"))
            {
                this.Close();
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}

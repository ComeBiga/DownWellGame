using UnityEngine;
using UnityEditor;

public class CatDownMakerNavigationWindow : EditorWindow
{
    static float windowWidth = 300;
    static float windowHeight = 400;

    [MenuItem("CatDownNavigation", menuItem = "4DX/CatDownNavigation")]
    public static void ConversationSystem()
    {
        var window = GetWindow<CatDownMakerNavigationWindow>(true, "CatDown Navigation", true);
        window.maxSize = new Vector2(windowWidth, windowHeight);
        window.minSize = new Vector2(windowWidth, windowHeight);

    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField(new GUIContent("CatDownMaker"), EditorStyles.boldLabel, GUILayout.Width(200), GUILayout.Height(50));

        EditorGUILayout.BeginVertical();
        {
            if(GUILayout.Button("StartScene"))
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/0.Scenes/StartScene.unity");

                this.Close();
            }
            
            if(GUILayout.Button("GameScene"))
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/0.Scenes/GameScene.unity");

                this.Close();
            }
            
            if(GUILayout.Button("LevelEditor"))
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/0.Scenes/LevelEditor/LevelEditor.unity");

                this.Close();
            }

            if (GUILayout.Button("Setting"))
            {
                
            }
        }
        EditorGUILayout.EndVertical();
    }
}

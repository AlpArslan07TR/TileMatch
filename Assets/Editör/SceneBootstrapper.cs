using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SceneBootstrapper
{
    private const string PreviousSceneKey = "PreviousScene";
    private const string ShouldLoadBootstrapKey = "ShouldLoadBootstrap";
    private const string LoadBootstrapMenu = "GD101/Load Boot Scene On Play";
    private const string DontLoadBootstrapMenu = "GD101/ Don't Load Boot Scene On Play";
    private static string BootstrapScene => EditorBuildSettings.scenes[0].path;

    private static string PreviousScene
    {
        get => EditorPrefs.GetString(PreviousSceneKey);
        set => EditorPrefs.SetString(PreviousSceneKey, value);
    }

    private static bool ShouldLoadBootstrapScene
    {
        get => EditorPrefs.GetBool(ShouldLoadBootstrapKey);
        set => EditorPrefs.SetBool(ShouldLoadBootstrapKey, value);
    }

    static SceneBootstrapper()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    static void OnPlayModeStateChanged(PlayModeStateChange playModeState)
    {
        if (!ShouldLoadBootstrapScene)
        {

        }

        switch (playModeState)
        {
            case PlayModeStateChange.ExitingEditMode:
                PreviousScene = SceneManager.GetActiveScene().path;
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()&&IsSceneInBuildSettings(BootstrapScene))
                
                {
                    EditorSceneManager.OpenScene(BootstrapScene);
                }
                break;

            case PlayModeStateChange.EnteredEditMode:
                if (!string.IsNullOrEmpty(PreviousScene))
                {
                    EditorSceneManager.OpenScene(PreviousScene);
                }
                break;
        }
        static bool IsSceneInBuildSettings(string bootstrapScene)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public SceneData SceneSetting;

    private Dictionary<string, string> scenePathSetting;

    private void Awake()
    {
        if (SceneController.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        scenePathSetting = new Dictionary<string, string>();
        if (SceneSetting != null)
        {
            foreach (var item in SceneSetting.ScenePaths)
            {
                if (scenePathSetting.ContainsKey(item.CurrentScene)) continue;
                scenePathSetting.Add(item.CurrentScene, item.NextScene);
            }
        }
        else
        {
            Debug.LogWarning("No scene setting!!");
        }
    }

    public string NextScene(string i_currentSceneName)
    {
        if (scenePathSetting.ContainsKey(i_currentSceneName)) return scenePathSetting[i_currentSceneName];
        
        return null;
    }
}

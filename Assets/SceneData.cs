using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenePath
{
    public string CurrentScene;
    public string NextScene;
}
[CreateAssetMenu(fileName = "NewScenePath", menuName = "CreateScenePath")]
public class SceneData : ScriptableObject
{
    public List<string> StageNames;
    public List<ScenePath> ScenePaths;
}

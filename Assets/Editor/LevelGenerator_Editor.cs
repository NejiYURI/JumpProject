using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGenerator_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelGenerator _tileG = (LevelGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            _tileG.ReadMap();
        }

        if (GUILayout.Button("Create File"))
        {
            _tileG.CreateFile();
        }
    }
}

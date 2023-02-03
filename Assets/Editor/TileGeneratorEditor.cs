using UnityEngine;
using UnityEditor;
using CustomTileSystem;

[CustomEditor(typeof(TileManager))]
public class TileGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TileManager _tileG = (TileManager)target;
        if (GUILayout.Button("Generate"))
        {
            _tileG.GenerateBaseTile();
        }

        if (GUILayout.Button("Clear"))
        {
            _tileG.ClearBaseTile();
        }
    }
}

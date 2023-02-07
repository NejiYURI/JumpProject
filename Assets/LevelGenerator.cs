using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ColorToTileData
{
    public string name;
    public Color color;
    public TileData tileData;
    public bool IsStartPoint;
}

public class LevelGenerator : MonoBehaviour
{
    public Texture2D mapImage;
    public List<ColorToTileData> ColorToTileDatas;
    [SerializeField]
    private List<LevelTile> levelTiles;
    [SerializeField]
    private Vector2Int StartLocation;

    public void ReadMap()
    {
        if (mapImage == null) return;
        levelTiles = new List<LevelTile>();
        for (int x = 0; x < mapImage.width; x++)
        {
            for (int y = 0; y < mapImage.height; y++)
            {
                ColorToTile(x, y);
            }
        }
        if (TileManager.tileManager != null) TileManager.tileManager.GenerateBySetupTiles(levelTiles);
    }

    void ColorToTile(int x, int y)
    {
        Color pixelColor = mapImage.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }

        foreach (var item in ColorToTileDatas)
        {
            if (item.color.Equals(pixelColor))
            {
                Debug.Log(item.name);
                if (item.IsStartPoint) StartLocation = new Vector2Int(x * -1, y);
                LevelTile tmpData = new LevelTile();
                tmpData.gridVector = new Vector2Int(x*-1, y);
                tmpData.tileData = item.tileData;
                levelTiles.Add(tmpData);
            }
        }
    }

    public void CreateFile()
    {
        if (levelTiles.Count > 0)
        {
            LevelData newFile = ScriptableObject.CreateInstance<LevelData>();
            newFile.TileData = new List<LevelTile>();
            newFile.TileData.AddRange(levelTiles);
            newFile.StartLocation = StartLocation;
#if UNITY_EDITOR
            var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/NewMap.asset");
            AssetDatabase.CreateAsset(newFile, uniqueFileName);
            //EditorUtility.SetDirty(newFile);
            AssetDatabase.SaveAssets();
#endif
        }
    }
}


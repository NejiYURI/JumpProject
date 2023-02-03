using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTile
{
    public Vector2Int gridVector;
    public TileData tileData;

}
public class LevelData : ScriptableObject
{
    public string LevelName;
    public Vector2Int StartLocation;
    public List<LevelTile> TileData;
}

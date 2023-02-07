using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEndTile", menuName = "TileObj/EndTile")]
public class EndTile : TileData
{
    public override void TileInit(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.TileImage);
            curObj.IsLight = true;
        }
    }
}

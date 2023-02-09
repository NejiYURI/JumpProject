using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRangeTile", menuName = "TileObj/RangeTile")]
public class RangeTile : TileData
{
    public override void TileInit(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.TileImage);
        }
        TileHide(curObj);
    }

    public override void TileShow(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().ShowSprite(true);
            curObj.IsLight = true;
        }
    }

    public override void TileHide(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().ShowSprite(false);
            curObj.IsLight = false;
        }
    }

}

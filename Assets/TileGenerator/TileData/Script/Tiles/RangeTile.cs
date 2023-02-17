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
        TileHide(curObj, true);
    }

    public override void TileShow(TileGridData curObj, bool IsInitial = false)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().ShowSprite(true, IsInitial,0.4f);
        }
    }

    public override void TileHide(TileGridData curObj, bool IsInitial = false)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().ShowSprite(false, IsInitial);
        }
    }

}

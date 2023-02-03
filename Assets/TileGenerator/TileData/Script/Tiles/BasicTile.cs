using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBasicTile", menuName = "TileObj/BasicTile")]
public class BasicTile : TileData
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

    public override void TileFlipFlop(TileGridData curObj)
    {
        if (curObj.IsLight)
        {
            TileHide(curObj);
        }
        else
        {
            TileShow(curObj);
        }
    }
}

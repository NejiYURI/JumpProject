using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPuzzleTile", menuName = "TileObj/PuzzleTile")]
public class PuzzleTile : TileData
{
    public Sprite LightImage;
    public override void TileInit(TileGridData curObj)
    {
        TileHide(curObj);

    }


    public override void TileShow(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.LightImage);
        }
    }

    public override void TileHide(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.TileImage);
        }
    }

    public override void TileFlipping(TileGridData curObj)
    {
        if (curObj.IsFlipping)
        {
            if (curObj.GetTileScript().CurrentSprite() == this.TileImage)
            {
                TileShow(curObj);
            }
            else
            {
                TileHide(curObj);
            }
        }
    }

    public override void TileTriggered(TileGridData curObj)
    {
        TileHide(curObj);
    }
}

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


    public override void TileShow(TileGridData curObj, bool IsInitial = false)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.LightImage);
        }
    }

    public override void TileHide(TileGridData curObj, bool IsInitial = false)
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
                if (GameEventManager.instance != null) GameEventManager.instance.PuzzleTrigger.Invoke(curObj.GridPosition,true);
            }
            else
            {
                TileHide(curObj);
                if (GameEventManager.instance != null) GameEventManager.instance.PuzzleTrigger.Invoke(curObj.GridPosition, false);
            }
        }
    }

    public override void TileTriggered(TileGridData curObj)
    {
        TileHide(curObj);
    }
}

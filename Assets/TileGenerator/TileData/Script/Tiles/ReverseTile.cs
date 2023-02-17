using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewReverseTile", menuName = "TileObj/NewReverseTile")]
public class ReverseTile : TileData
{

    public override void TileInit(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.TileImage);
        }
        TileShow(curObj, true);
    }

    public override void TileShow(TileGridData curObj, bool IsInitial = false)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().ShowSprite(true, IsInitial);
        }
    }

    public override void TileHide(TileGridData curObj, bool IsInitial = false)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().ShowSprite(false, IsInitial);
        }
    }

    public override void TileFlipping(TileGridData curObj)
    {
        if (curObj.CharacterOnTile != null && curObj.CharacterOnTile.IsPlayer) return;
        if (curObj.IsFlipping)
        {
            TileHide(curObj);
        }
        else
        {
            TileShow(curObj);
        }
    }

    public override void CharacterInTile(TileGridData curObj, bool IsPlayer)
    {
        if (IsPlayer) TileShow(curObj, true);
    }

    public override void CharacterLeaveTile(TileGridData curObj)
    {
        //TileHide(curObj);
    }

    public override bool GetCanMove(TileGridData curObj)
    {
        return !curObj.IsFlipping;
    }
}

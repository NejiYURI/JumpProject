using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTriggerTile", menuName = "TileObj/TriggerTile")]
public class TriggerTile : TileData
{
    public bool ShowInStart;

    public override void TileInit(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.TileImage);
        }
        if (!ShowInStart)
        {
            TileHide(curObj, true);
        }
            
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

    public override void CharacterInTile(TileGridData curObj, bool IsPlayer)
    {

    }

    public override void CharacterLeaveTile(TileGridData curObj)
    {
    }

    public override void TileFlipping(TileGridData curObj)
    {

    }

    public override bool GetCanMove(TileGridData curObj)
    {
        return curObj.TriggerFlipFlop;
    }

    public override void TileTriggered(TileGridData curObj)
    {
        curObj.TriggerFlipFlop = !curObj.TriggerFlipFlop;
        if (curObj.TriggerFlipFlop) TileShow(curObj);
        else TileHide(curObj);
    }
}

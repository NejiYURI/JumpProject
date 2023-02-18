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
        }
    }

    public override void CharacterInTile(TileGridData curObj, bool IsPlayer)
    {
        if (IsPlayer && GameEventManager.instance != null) GameEventManager.instance.StageClear.Invoke();
    }
}

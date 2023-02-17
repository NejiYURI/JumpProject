using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewButtonTile", menuName = "TileObj/ButtonTile")]
public class ButtonTile : TileData
{

    public override void TileInit(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.TileImage);
        }
    }

    public override void TileStomp(TileGridData curObj)
    {
        if (GameEventManager.instance != null && !string.IsNullOrEmpty(curObj.GetTileScript().TriggerId)) GameEventManager.instance.TileTrigger.Invoke(curObj.GetTileScript().TriggerId);
    }
}

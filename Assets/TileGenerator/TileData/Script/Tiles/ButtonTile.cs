using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewButtonTile", menuName = "TileObj/ButtonTile")]
public class ButtonTile : TileData
{
    public string TriggerId;

    public override void TileInit(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.TileImage);
        }
    }

    public override void TileStomp()
    {
        if (GameEventManager.instance != null && !string.IsNullOrEmpty(TriggerId)) GameEventManager.instance.TileTrigger.Invoke(TriggerId);
    }
}

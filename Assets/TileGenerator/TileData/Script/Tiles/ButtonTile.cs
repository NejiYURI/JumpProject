using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewButtonTile", menuName = "TileObj/ButtonTile")]
public class ButtonTile : TileData
{
    public AudioClip ButtonSound;
    public override void TileInit(TileGridData curObj)
    {
        if (curObj.GetTileScript() != null)
        {
            curObj.GetTileScript().TileSpriteSet(this.TileImage);
        }
    }

    public override void TileStomp(TileGridData curObj)
    {
        if (AudioController.instance != null) AudioController.instance.PlaySound(ButtonSound, 0.8f);
        if (GameEventManager.instance != null && !string.IsNullOrEmpty(curObj.GetTileScript().TriggerId)) GameEventManager.instance.TileTrigger.Invoke(curObj.GetTileScript().TriggerId);
    }
}

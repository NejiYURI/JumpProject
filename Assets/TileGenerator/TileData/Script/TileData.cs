using CustomTileSystem;
using UnityEngine;

public class TileData : ScriptableObject
{
    public Sprite TileImage;

    public virtual void TileInit(TileGridData curObj)
    {

    }

    public virtual void TileShow(TileGridData curObj)
    {

    }

    public virtual void TileHide(TileGridData curObj)
    {

    }

    public virtual void CanLight(TileGridData curObj)
    {

    }

    public virtual void CharacterInTile(TileGridData curObj, bool IsPlayer)
    {
       
    }

    public virtual void CharacterLeaveTile(TileGridData curObj)
    {
      
    }

    public virtual void TileFlipping(TileGridData curObj)
    {
        
    }

    public virtual bool GetCanMove(TileGridData curObj)
    {
        return true;
    }

    public virtual void TileTriggered(TileGridData curObj)
    {

    }

    public virtual void TileStomp()
    {

    }
}

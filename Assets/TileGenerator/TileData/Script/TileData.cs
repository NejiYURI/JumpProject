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

    public virtual void TileFlipFlop(TileGridData curObj)
    {

    }

    public virtual void CanLight(TileGridData curObj)
    {

    }
}

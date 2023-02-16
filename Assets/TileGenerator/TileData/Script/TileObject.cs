using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TileObject : MonoBehaviour
{

    public TileData TileData;

    public TileGridData gridData;

    public string ObjId;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (GameEventManager.instance != null) GameEventManager.instance.TileTrigger.AddListener(TileTrigger);
    }

    public void TileSet()
    {
        if (this.spriteRenderer == null) this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = this.TileData.TileImage;
    }

    public void TileSet(TileData _tiledata)
    {
        if (this.spriteRenderer == null) this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.TileData = _tiledata;
        this.spriteRenderer.sprite = this.TileData.TileImage;
    }

    public void TileSpriteSet(Sprite i_sprite)
    {
        if (this.spriteRenderer == null) this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = i_sprite;
    }

    public void ShowSprite(bool i_Set)
    {
        this.spriteRenderer.enabled = i_Set;
    }
    public void ShowSprite_FlipFlop()
    {
        this.spriteRenderer.enabled = !this.spriteRenderer.enabled;
    }

    public Sprite CurrentSprite()
    {
        return this.spriteRenderer.sprite;
    }

    void TileTrigger(string i_objId)
    {
        if (TileData == null || gridData == null) return;
        if (i_objId.Equals(ObjId)) TileData.TileTriggered(gridData);
    }
}

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

    public string TriggerId;

    private float ShowSpd=0.1f;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        GetSelfSpriteRenderer();
        if (GameEventManager.instance != null) GameEventManager.instance.TileTrigger.AddListener(TileTrigger);
    }

    public void TileSpriteSet(Sprite i_sprite)
    {
        if (this.spriteRenderer == null) GetSelfSpriteRenderer();
        this.spriteRenderer.sprite = i_sprite;
    }

    public void ShowSprite(bool i_Set, bool IsInitial = false, float MaxOpacity=1f)
    {
        //this.spriteRenderer.enabled = i_Set;
        if (!IsInitial)
            StartCoroutine(SpriteShowCoroutine(!i_Set));
        else
            this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, i_Set ? MaxOpacity : 0f);
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

    public void GetSelfSpriteRenderer()
    {
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    IEnumerator SpriteShowCoroutine(bool i_reverse)
    {
        float cnt = 0;
        while (cnt <= ShowSpd)
        {
            this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, i_reverse ? 1f - cnt / ShowSpd : cnt / ShowSpd);
            yield return null;
            cnt += Time.deltaTime;
        }
        this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, i_reverse ? 0f : 1f);
    }
}

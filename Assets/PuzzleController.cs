using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PuzzleCodeData
{
    public PuzzleCodeData(Vector2Int i_VectorPos, bool i_IsOpen)
    {
        this.VectorPos = i_VectorPos;
        this.IsOpen = i_IsOpen;
    }
    public Vector2Int VectorPos;
    public bool IsOpen;
}
public class PuzzleController : MonoBehaviour
{
    public string ChkId;

    public string SuccessId;

    public string ResetTileId;



    public List<PuzzleCodeData> PuzzleCodes;

    [SerializeField]
    private List<PuzzleCodeData> tmp_PuzzleCodes;
    // Start is called before the first frame update
    void Start()
    {
        this.tmp_PuzzleCodes = new List<PuzzleCodeData>();

        foreach (var item in PuzzleCodes)
        {
            tmp_PuzzleCodes.Add(new PuzzleCodeData(item.VectorPos, false));
        }
        if (GameEventManager.instance)
        {
            GameEventManager.instance.TileTrigger.AddListener(ChkPzl);
            GameEventManager.instance.PuzzleTrigger.AddListener(PuzzleChange);
        }
    }

    void PuzzleChange(Vector2Int i_VectorPos, bool i_IsOpen)
    {
        if (tmp_PuzzleCodes.Where(x => x.VectorPos == i_VectorPos).Count() > 0)
        {
            tmp_PuzzleCodes.Where(x => x.VectorPos == i_VectorPos).First().IsOpen = i_IsOpen;
        }
    }

    void ChkPzl(string id)
    {
        if (!id.Equals(ChkId)) return;
        bool success = true;
        foreach (var item in tmp_PuzzleCodes)
        {
            if (PuzzleCodes.Where(x => x.VectorPos == item.VectorPos && x.IsOpen == item.IsOpen).Count() <= 0)
            {
                success = false;
                break;
            }
        }
        if (success)
        {
            if (GameEventManager.instance)
            {
                GameEventManager.instance.TileTrigger.Invoke(SuccessId);
            }
        }
        else
        {
            if (GameEventManager.instance)
            {
                GameEventManager.instance.TileTrigger.Invoke(ResetTileId);
            }
            foreach (var item in tmp_PuzzleCodes)
            {
                item.IsOpen= false;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTileSystem
{
    public class TileInteractScript : MonoBehaviour
    {
        public static TileInteractScript tileInteract;

        private TileManager tileManager;

        private List<Vector2Int> SelectList;

        public TileData tile_MoveSelect;

        public TileData tile_AtkSelect;

        private Vector2Int? PrevGridPos = null;
        public AnimationCurve WaveCurve;
        [Range(1, 5)]
        public float WaveSpeed = 1f;


        private void Awake()
        {
            tileInteract = this;
        }

        private void Start()
        {
            if (TileManager.tileManager != null)
                tileManager = TileManager.tileManager;
            PrevGridPos = null;
        }
        // Update is called once per frame
        void Update()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int GridPos = tileManager.ToGridVector(mousePosition);
            if (tileManager.HasTile(GridPos) && mousePosition != PrevGridPos)
            {
                if (PrevGridPos != null)
                    TileManager.tileManager.ResetTile((Vector2Int)PrevGridPos);
                TileManager.tileManager.ActiveTile(GridPos);
                PrevGridPos = GridPos;
            }
            else if (GridPos != PrevGridPos && PrevGridPos != null)
            {
                tileManager.ResetTile((Vector2Int)PrevGridPos);
                PrevGridPos = null;
            }
        }

        public void SelectedRange(Vector2Int CenterPos, int Range, bool IsMove)
        {
            if (TileManager.tileManager == null) return;
            SelectList = new List<Vector2Int>();
            int CurRange = Range;
            for (int x = 0; x <= Range; x++)
            {
                for (int y = CurRange - 1; y > -CurRange; y--)
                {
                    if (CenterPos + new Vector2Int(x, y) == CenterPos) continue;
                    if (TileManager.tileManager.HasTile(CenterPos + new Vector2Int(x, y)) && (!IsMove || !TileManager.tileManager.GetTileIsBlock(CenterPos + new Vector2Int(x, y))))
                    {
                        TileManager.tileManager.SetSelectTileStyle(IsMove ? this.tile_MoveSelect : this.tile_AtkSelect);
                        TileManager.tileManager.SelectTile(CenterPos + new Vector2Int(x, y));
                        SelectList.Add(CenterPos + new Vector2Int(x, y));
                    }
                    if (x != 0)
                        if (TileManager.tileManager.HasTile(CenterPos - new Vector2Int(x, y)) && (!IsMove || !TileManager.tileManager.GetTileIsBlock(CenterPos - new Vector2Int(x, y))))
                        {
                            TileManager.tileManager.SetSelectTileStyle(IsMove ? this.tile_MoveSelect : this.tile_AtkSelect);
                            TileManager.tileManager.SelectTile(CenterPos - new Vector2Int(x, y));
                            SelectList.Add(CenterPos - new Vector2Int(x, y));
                        }
                }
                CurRange--;
            }


        }

        public void CancelSelectRange()
        {
            if (TileManager.tileManager == null) return;
            foreach (var item in SelectList)
            {
                TileManager.tileManager.CancelSelectTile(item);
            }
            SelectList = new List<Vector2Int>();
        }

        public bool CanSelect(Vector2Int i_targetVector)
        {
            return SelectList.Contains(i_targetVector);
        }

        public void StartWave(Vector2Int i_Center, int i_Range, bool CenterChangeShow = true)
        {
            StartCoroutine(TileWave(i_Center, i_Range, CenterChangeShow));
        }

        public void ReverseWave(Vector2Int i_Center, int i_Range, bool CenterChangeShow = true)
        {
            StartCoroutine(ReverseTileWave(i_Center, i_Range, CenterChangeShow));
        }

        IEnumerator TileWave(Vector2Int i_Center, int i_Range, bool CenterChangeShow = true)
        {
            if (TileManager.tileManager != null)
            {
                Dictionary<int, List<Vector2Int>> Tiles = TileManager.tileManager.GetListOfRange(i_Center);
                for (int index = 0; index < i_Range; index++)
                {
                    if (Tiles.ContainsKey(index))
                    {
                        foreach (Vector2Int grid in Tiles[index])
                        {
                            bool isSuccess = false;
                            StartCoroutine(SingleTileWave(TileManager.tileManager.GetTileData(grid, out isSuccess), ((grid == i_Center && CenterChangeShow) || grid != i_Center)));
                        }
                        yield return new WaitForSeconds(0.08f);
                    }
                }
            }
        }

        IEnumerator ReverseTileWave(Vector2Int i_Center, int i_Range, bool CenterChangeShow = true)
        {
            if (TileManager.tileManager != null)
            {
                Dictionary<int, List<Vector2Int>> Tiles = TileManager.tileManager.GetListOfRange(i_Center);
                for (int index = i_Range-1; index >= 0; index--)
                {
                    if (Tiles.ContainsKey(index))
                    {
                        foreach (Vector2Int grid in Tiles[index])
                        {
                            bool isSuccess = false;
                            StartCoroutine(SingleTileWave(TileManager.tileManager.GetTileData(grid, out isSuccess), ((grid == i_Center && CenterChangeShow) || grid != i_Center)));
                        }
                        yield return new WaitForSeconds(0.08f);
                    }
                }
            }

        }
        IEnumerator SingleTileWave(TileGridData TargetTile, bool ChangeShow)
        {
            Transform targetObj = TargetTile.GetTileGameObject().transform;
            if (ChangeShow) TargetTile.SetTileShow_FlipFlop();
            Vector2 pos = (Vector2)targetObj.position;
            float Timer = 0;
            while (Timer <= 1f)
            {
                targetObj.position = pos + new Vector2(0, WaveCurve.Evaluate(Timer));
                if (Timer >= 1) break;
                yield return new WaitForSeconds(Time.fixedDeltaTime / WaveSpeed);
                Timer += Time.fixedDeltaTime;
                Timer = Mathf.Clamp(Timer, -99, 1);
            }
            targetObj.position = pos;
        }
    }
}

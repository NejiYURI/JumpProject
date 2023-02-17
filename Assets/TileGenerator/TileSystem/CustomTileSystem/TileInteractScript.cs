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
        }


        public void StartWave(Vector2Int i_Center, int i_Range)
        {
            if (TileManager.tileManager != null)
            {
                TileManager.tileManager.StompTile(i_Center);
            }
            StartCoroutine(TileWave(i_Center, i_Range));
        }

        public void ReverseWave(Vector2Int i_Center, int i_Range)
        {
            StartCoroutine(ReverseTileWave(i_Center, i_Range));
        }

        IEnumerator TileWave(Vector2Int i_Center, int i_Range)
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
                            StartCoroutine(SingleTileWave(TileManager.tileManager.GetTileData(grid, out isSuccess), true));
                        }
                        yield return new WaitForSeconds(0.08f);
                    }
                }
            }
        }

        IEnumerator ReverseTileWave(Vector2Int i_Center, int i_Range)
        {
            if (TileManager.tileManager != null)
            {
                Dictionary<int, List<Vector2Int>> Tiles = TileManager.tileManager.GetListOfRange(i_Center);
                for (int index = i_Range - 1; index >= 0; index--)
                {
                    if (Tiles.ContainsKey(index))
                    {
                        foreach (Vector2Int grid in Tiles[index])
                        {
                            bool isSuccess = false;
                            StartCoroutine(SingleTileWave(TileManager.tileManager.GetTileData(grid, out isSuccess), false));
                        }
                        yield return new WaitForSeconds(0.08f);
                    }
                }
            }

        }
        IEnumerator SingleTileWave(TileGridData TargetTile, bool IsFlipping)
        {
            Transform targetObj = TargetTile.GetTileGameObject().transform;
            TargetTile.SetTileFlipping(IsFlipping);
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

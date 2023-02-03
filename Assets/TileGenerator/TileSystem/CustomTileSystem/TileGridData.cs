using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CustomTileSystem
{
    public class TileGridData
    {
        public Vector2Int GridPosition;

        public Vector3 WorldLocation;

        private GameObject tileObject;

        private TileObject tileScript;

        private TileData tileData;

        public int G;
        public int H;

        public int F { get { return G + H; } }

        public bool IsBlocked;

        public TileGridData previousTile;

        public bool IsSelected;

        public bool IsLight;

        public IF_GameCharacter CharacterOnTile;

        public void SetTileObject(GameObject i_obj)
        {
            tileObject = i_obj;
            if (i_obj.GetComponent<TileObject>() != null)
            {
                tileObject = i_obj;
                tileScript = i_obj.GetComponent<TileObject>();
            }
        }

        public TileObject GetTileScript()
        {
            return tileScript;
        }

        public GameObject GetTileGameObject()
        {
            return tileObject;
        }

        public void SetTileData(TileData i_setData,bool initTile=false)
        {
            tileData = i_setData;
            if (initTile) tileData.TileInit(this);
        }

        public void SetTileShow(bool i_Set)
        {
            IsLight = i_Set;
            if (tileScript != null)
            {
                tileScript.ShowSprite(i_Set);
            }
        }

        public void SetTileShow_FlipFlop()
        {
            IsLight = !IsLight;
            if (tileScript != null)
            {
                tileScript.ShowSprite_FlipFlop();
            }
        }
    }
}


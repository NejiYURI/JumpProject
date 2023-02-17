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

        public bool IsFlipping;

        public IF_GameCharacter CharacterOnTile;

        public bool TriggerFlipFlop;

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

        public void SetCharacterInTile(IF_GameCharacter i_character)
        {
            CharacterOnTile = i_character;
            tileData.CharacterInTile(this, CharacterOnTile.IsPlayer);
        }

        public bool GetCanMove()
        {
            return tileData.GetCanMove(this);
        }

        public void SetCharacterLeaveTile()
        {
            CharacterOnTile = null;
            tileData.CharacterLeaveTile(this);
        }

        public void SetTileData(TileData i_setData, bool initTile = false)
        {
            tileData = i_setData;
            if (initTile) tileData.TileInit(this);
        }

        public void SetTileShow(bool i_Set,bool IsInitial=false)
        {
            if (tileData == null) return;
            if (i_Set)
                tileData.TileShow(this, IsInitial);
            else
                tileData.TileHide(this, IsInitial);
        }

        public void SetTileFlipping(bool i_isFlipping)
        {
            IsFlipping = i_isFlipping;
            if (tileData == null) return;
            tileData.TileFlipping(this);
        }

        public void StompTile()
        {
            if (tileData == null) return;
            tileData.TileStomp(this);
        }
    }
}


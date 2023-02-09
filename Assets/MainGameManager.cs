using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomTileSystem;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{

    public static MainGameManager mainGameManager;
    public GameObject PlayerObject;

    public LevelData levelData;

    public Vector2Int PlayerStartPos;

    private Vector2Int Player_Position;

    private void Awake()
    {
        mainGameManager = this;
    }
    private void Start()
    {
        if (TileManager.tileManager != null && levelData != null)
        {
            StartCoroutine(GameStartFunc());
        }
    }

    IEnumerator GameStartFunc()
    {
        yield return TileManager.tileManager.GenerateBySetupTiles(levelData.TileData);
        PlayerStartPos = levelData.StartLocation;
        SpawnCharacter(PlayerStartPos, PlayerObject, false);
        if (PlayerObject.GetComponent<PlayerScript>()) PlayerObject.GetComponent<PlayerScript>().SpawnRangeTile();
        if (TileManager.tileManager.HasTile(PlayerStartPos))
        {
            SetPlayerPos(PlayerStartPos);
        }
    }

    public GameObject SpawnObj(GameObject i_obj, Vector3 i_pos)
    {
        return Instantiate(i_obj, i_pos, Quaternion.identity);
    }

    public GameObject SpawnCharacter(Vector2Int i_pos, GameObject i_Character, bool SpawnNew = true)
    {
        bool IsSucces = false;
        #region-Custom Tile Ver
        TileGridData gridData = TileManager.tileManager.GetTileData(i_pos, out IsSucces);
        if (IsSucces)
        {
            //Debug.Log("Spawn " + i_Character.name);
            GameObject SpawnCharacter = SpawnNew ? SpawnObj(i_Character, gridData.WorldLocation) : i_Character;
            if (!SpawnNew) i_Character.transform.position = gridData.WorldLocation;
            if (SpawnCharacter.GetComponent<IF_GameCharacter>() != null)
            {
                TileManager.tileManager.CharacterInTile(gridData.GridPosition, SpawnCharacter.GetComponent<IF_GameCharacter>());
                SpawnCharacter.GetComponent<IF_GameCharacter>().TileVector = gridData.GridPosition;
            }
            return SpawnCharacter;
        }
        #endregion
        return null;
    }

    public Vector2Int GetPlayerPos()
    {
        return Player_Position;
    }

    public void SetPlayerPos(Vector2Int i_NewPos)
    {
        Player_Position = i_NewPos;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}


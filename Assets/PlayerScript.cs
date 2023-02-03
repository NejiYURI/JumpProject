using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour, IF_GameCharacter
{
    public Vector2Int TileVector;
    public bool IsPlayer;
    Vector2Int IF_GameCharacter.TileVector
    {
        get
        {
            return TileVector;
        }
        set
        {
            TileVector = value;
        }
    }

    bool IF_GameCharacter.IsPlayer
    {
        get
        {
            return IsPlayer;
        }
        set
        {
            IsPlayer = value;
        }
    }

    private PlayerControl inputActions;

    private bool CanJump;

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Awake()
    {
        inputActions = new PlayerControl();
    }

    private void Start()
    {
        inputActions.BasicControl.Action.performed += _ => JumpFunc();
        inputActions.BasicControl.Up.performed += _ => MovementInput(new Vector2Int(1, 0));
        inputActions.BasicControl.Down.performed += _ => MovementInput(new Vector2Int(-1, 0));
        inputActions.BasicControl.Left.performed += _ => MovementInput(new Vector2Int(0, -1));
        inputActions.BasicControl.Right.performed += _ => MovementInput(new Vector2Int(0, 1));
        CanJump = true;
    }

    void JumpFunc()
    {
        if (!CanJump) return;
        if (TileInteractScript.tileInteract != null)
        {
            StartCoroutine(LightFunc());
        }
    }

    void MovementInput(Vector2Int i_dir)
    {
        if (TileManager.tileManager != null)
        {
            if (TileManager.tileManager.HasTile(TileVector + i_dir))
            {
                bool isSuccess = false;
                
                if (TileManager.tileManager.GetTileData(TileVector + i_dir, out isSuccess).IsLight)
                {
                    TileManager.tileManager.CharacterLeaveTile(TileVector);
                    transform.LeanMove(TileManager.tileManager.GetTileWorldPosition(TileVector + i_dir, out isSuccess), 0.1f);
                    this.TileVector = TileVector + i_dir;
                    TileManager.tileManager.CharacterInTile(TileVector, this);
                    if (MainGameManager.mainGameManager != null)
                    {
                        MainGameManager.mainGameManager.SetPlayerPos(TileVector);
                    }
                }
                
            }
        }
    }

    IEnumerator LightFunc()
    {
        this.CanJump = false;
        Vector2Int tmpVec = TileVector;
        TileInteractScript.tileInteract.StartWave(tmpVec, 4);
        yield return new WaitForSeconds(3f);
        TileInteractScript.tileInteract.ReverseWave(tmpVec, 4);
        this.CanJump = true;
    }
}

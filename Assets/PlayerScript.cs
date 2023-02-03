using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour, IF_GameCharacter
{
    public Vector2Int TileVector;
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

    IEnumerator LightFunc()
    {
        this.CanJump = false;
        TileInteractScript.tileInteract.StartWave(TileVector, 4, false);
        yield return new WaitForSeconds(3f);
        TileInteractScript.tileInteract.ReverseWave(TileVector, 4, false);
        this.CanJump = true;
    }
}

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
    private Coroutine ChargeCoroutine;

    public GameObject ChargeTileObj;
    public TileData ChargeTileData;
    [Range(1, 10)]
    public int ChargeMaxRange = 5;
    [Range(1, 10)]
    public float ChargeSpeed = 2f;
    [SerializeField]
    private float ChargeCounter;

    private Dictionary<int, List<TileGridData>> RangeTileList;
    public Animator animator;
    public SpriteRenderer CharacterSprite;
    public AudioClip MoveSound;
    public AudioClip JumpSound;
    public AudioClip StompSound;
    public AudioClip ChargeSound;
    private bool CanJump;
    private bool Jumping;
    private bool JumpCharging;

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
        inputActions.BasicControl.Action.canceled += _ => JumpCancel();
        inputActions.BasicControl.Up.performed += _ => MovementInput(new Vector2Int(1, 0));
        inputActions.BasicControl.Down.performed += _ => MovementInput(new Vector2Int(-1, 0));
        inputActions.BasicControl.Left.performed += _ => MovementInput(new Vector2Int(0, -1));
        inputActions.BasicControl.Right.performed += _ => MovementInput(new Vector2Int(0, 1));
        CanJump = true;
    }

    void JumpFunc()
    {
        if (!CanJump || Jumping) return;
        JumpCharging = true;
        //Debug.Log(JumpCharging);
        if (AudioController.instance != null) AudioController.instance.PlaySound(ChargeSound, 0.25f);
        if (JumpCharging) ChargeCoroutine = StartCoroutine(JumpCharge());
    }

    void JumpCancel()
    {
        if (!CanJump || Jumping) return;
        JumpCharging = false;

    }

    void MovementInput(Vector2Int i_dir)
    {
        if (i_dir.y != 0f && CharacterSprite!=null)
        {
            CharacterSprite.flipX = i_dir.y > 0f;
        }
        if (JumpCharging || Jumping) return;
        if (TileManager.tileManager != null)
        {
            if (TileManager.tileManager.HasTile(TileVector + i_dir))
            {
                bool isSuccess = false;

                if (TileManager.tileManager.GetTileData(TileVector + i_dir, out isSuccess).GetCanMove())
                {
                    TileManager.tileManager.CharacterLeaveTile(TileVector);
                    if (animator != null)
                    {
                        animator.SetTrigger("Move");
                    }
                    if (AudioController.instance != null) AudioController.instance.PlaySound(MoveSound,0.5f);
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

    public void SpawnRangeTile()
    {
        if (TileManager.tileManager != null)
        {
            TileManager.tileManager.GenerateCustomTile(this.transform, this.ChargeTileObj, this.TileVector, this.ChargeTileData, ChargeMaxRange, out RangeTileList);
        }
    }

    void ShowChargeRange(int Range, bool i_Show)
    {
        for (int i = 1; i < Range; i++)
        {
            foreach (var item in RangeTileList[i])
            {
                item.SetTileShow(i_Show,true);
            }
        }
    }

    IEnumerator LightFunc(int i_Range)
    {
        if (AudioController.instance != null) AudioController.instance.PlaySound(StompSound, 0.5f);
        //Debug.Log("Show range:" + i_Range);
        this.CanJump = false;
        Vector2Int tmpVec = TileVector;
        TileInteractScript.tileInteract.StartWave(tmpVec, i_Range);
        yield return new WaitForSeconds(2f);
        TileInteractScript.tileInteract.ReverseWave(tmpVec, i_Range);
        this.CanJump = true;
       
    }

    IEnumerator JumpCharge()
    {
        while (JumpCharging)
        {
            ChargeCounter = Mathf.Clamp(ChargeCounter + Time.deltaTime * ChargeSpeed, 0f, ChargeMaxRange - 2f);
            ShowChargeRange(Mathf.FloorToInt(ChargeCounter + 2f), true);
            if (animator != null) animator.SetBool("Charging", true);
            yield return null;

        }
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
        if (AudioController.instance != null) AudioController.instance.PlaySound(JumpSound, 0.25f);
        Jumping = true;
        ShowChargeRange(Mathf.FloorToInt(ChargeMaxRange), false);

    }

    public void Stomp()
    {
        if (animator != null) animator.SetBool("Charging", false);
        StartCoroutine(LightFunc(Mathf.FloorToInt(ChargeCounter + 2f)));
        ChargeCounter = 0;
    }

    public void CanMove()
    {
        Jumping = false;
    }
}

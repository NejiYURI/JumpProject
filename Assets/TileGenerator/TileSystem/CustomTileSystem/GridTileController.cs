using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTileSystem;
public class GridTileController : MonoBehaviour
{

    public TileManager tileManager;

    public TileData Select_TileData;

    private Vector2 PrevPos;

    [SerializeField]
    private Vector2 mousePos;

    public GameObject MainCharacter;

    private void Start()
    {
        //if (tileManager != null)
        //{
        //    Vector2 getpos = tileManager.GetTileVector(2);
        //    Debug.Log(getpos);
        //    if (getpos != null)
        //    {
        //        Instantiate(MainCharacter, (Vector2)getpos, Quaternion.identity);
        //    }

        //}
    }


    void Update()
    {
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition = tileManager.ToGridVector(mousePosition);
        //mousePos = mousePosition;
        //if (tileManager.HasTile(mousePosition) && mousePosition != PrevPos)
        //{
        //    if (PrevPos != null) tileManager.ResetTile(PrevPos);
        //    tileManager.SetTile(mousePosition, Select_TileData);
        //    PrevPos = mousePosition;
        //}
        //else if (mousePosition != PrevPos && PrevPos != null)
        //{
        //    tileManager.ResetTile(PrevPos);
        //    PrevPos = null;
        //}
    }


}

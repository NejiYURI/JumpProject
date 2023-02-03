using UnityEngine;

public interface IF_GameCharacter
{
    bool IsPlayer { get; set; }
    Vector2Int TileVector { get; set; }
}

using UnityEngine;

class Isometric_Matrix
{
    public Isometric_Matrix()
    {
        this.i_hat = new Vector2(-0.5f, 0.25f);
        this.j_hat = new Vector2(0.5f, 0.25f);
    }
    public Isometric_Matrix(Vector2 _ihat, Vector2 _jhat)
    {
        this.i_hat = _ihat;
        this.j_hat = _jhat;
    }
    public Vector2 i_hat;
    public Vector2 j_hat;
}


public abstract class LevelObject
{
    public Vector2Int Pos;

    public GameObject obj;
}
[System.Serializable]
public class LevelCharacter : LevelObject
{
    
}
[System.Serializable]
public class LevelObstacle : LevelObject
{

}


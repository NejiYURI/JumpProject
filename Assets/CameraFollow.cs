using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 Offset;
    public Transform TargetObj;
    public float LateTime;
    public float LateTimeMul;
    public bool CanMove;

    private void Start()
    {
        //this.transform.position = TargetObj.position + Offset;
    }

    private void FixedUpdate()
    {
        if (TargetObj != null)
            this.transform.position = Vector3.Lerp(this.transform.position, TargetObj.position + Offset, LateTime);
    }
}

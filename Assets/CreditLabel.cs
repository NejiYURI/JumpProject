using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditLabel : MonoBehaviour
{
    public string ObjId;
    public bool IsShow;
    private void Start()
    {
        if (GameEventManager.instance) GameEventManager.instance.TileTrigger.AddListener(ShowText);
        this.transform.LeanScale(IsShow ? new Vector3(0.1f,0.1f,0.1f): Vector3.zero, 0f);
    }

    void ShowText(string i_objId)
    {
        if (i_objId.Equals(this.ObjId))
        {
            IsShow = !IsShow;
            this.transform.LeanScale(IsShow ? new Vector3(0.1f, 0.1f, 0.1f) : Vector3.zero, 0.1f);
        }
    }
}

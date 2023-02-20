using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform TargetTransform;

    public Camera cameraControl;
    public Color StartColor;
    public Color EndColor;
    public float MaxDistance = 1f;

    public Image WhiteUI;
    public float WhiteTime;

    private void Start()
    {
        if (cameraControl != null) cameraControl.backgroundColor = StartColor;
        if (GameEventManager.instance) GameEventManager.instance.StageClear.AddListener(StageClear);
    }
    private void FixedUpdate()
    {
        float dis = Vector2.Distance(TargetTransform.position, playerTransform.position);
        if (cameraControl != null) cameraControl.backgroundColor = Color.Lerp(StartColor, EndColor, Mathf.Clamp(1f - (dis / MaxDistance), 0f, 1f));
    }
    void StageClear()
    {
        StartCoroutine(WhiteTimer());
    }
    IEnumerator WhiteTimer()
    {
        float cnt = 0;
        while (cnt <= WhiteTime)
        {
            this.WhiteUI.color = new Color(this.WhiteUI.color.r, this.WhiteUI.color.g, this.WhiteUI.color.b, cnt / WhiteTime);
            yield return null;
            cnt += Time.deltaTime;
        }
        this.WhiteUI.color = new Color(this.WhiteUI.color.r, this.WhiteUI.color.g, this.WhiteUI.color.b, 1f);
    }
}

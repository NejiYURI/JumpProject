using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StageBtnScript : MonoBehaviour
{
    public string StageName;
    public TextMeshProUGUI NameTxt;
    void Start()
    {
        
    }

    public void InitialSet(string i_stageName)
    {
        this.StageName = i_stageName;
        NameTxt.text = StageName;
    }

    public void PressFunc()
    {
        if (TitleScript.instance != null) TitleScript.instance.LoadScene(StageName);
    }
}

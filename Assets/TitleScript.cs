using CustomTileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public static TitleScript instance;
    public Vector2 Center;
    public Vector2 Size;
    public Transform CameraObj;
    public Transform CameraEndPos;
    public GameObject UIPanel;
    public Transform StageList;
    public GameObject StageBtn;
  


    public GameObject StageSelectBtn;
    public GameObject BasicMenu;
    public GameObject StageMenu;

    private Dictionary<Vector2Int, int> ActivatingTile;

    private Dictionary<string, int> UnlockStages;

    public int MaxWaveSize;

    private bool GameStartMove;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ActivatingTile = new Dictionary<Vector2Int, int>();
        UnlockStages = new Dictionary<string, int>();
        ShowBasicMenu();
        if (SceneController.instance != null)
        {
            List<string> stages = SceneController.instance.GetStages();
            foreach (var item in stages)
            {
                UnlockStages.Add(item, PlayerPrefs.GetInt(item, 0));
            }
        }
        SetStageList();
        StartCoroutine(WaveTimer());
    }

    private void FixedUpdate()
    {
        if (GameStartMove)
            CameraObj.position = Vector3.Lerp(CameraObj.position, CameraEndPos.position, 0.02f);
    }

    void SetStageList()
    {
        bool CanShowBtn = false;
        foreach (var item in UnlockStages)
        {
            Debug.Log(item.Key + ":" + item.Value);
            if (item.Value == 1)
            {
                CanShowBtn = true;
                GameObject btn = Instantiate(StageBtn, StageList.position, Quaternion.identity);
                btn.transform.SetParent(StageList, false);
                if (btn.GetComponent<StageBtnScript>()) btn.GetComponent<StageBtnScript>().InitialSet(item.Key);
            }
        }
        //StartCoroutine(UpdateLayoutGroup());
        StageSelectBtn.SetActive(CanShowBtn);
    }

    IEnumerator UpdateLayoutGroup()
    {
        StageList.GetComponent<GridLayoutGroup>().enabled = false;
        LayoutRebuilder.ForceRebuildLayoutImmediate(StageList.GetComponent<RectTransform>());
        yield return new WaitForEndOfFrame();
        StageList.GetComponent<GridLayoutGroup>().enabled = true;
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Title");
    }

    public void ShowStageMenu()
    {
        this.BasicMenu.SetActive(false);
        this.StageMenu.SetActive(true);
    }

    public void ShowBasicMenu()
    {
        this.BasicMenu.SetActive(true);
        this.StageMenu.SetActive(false);
    }

    public void LoadScene(string SceneName)
    {
        GameStartMove = true;
        UIPanel.SetActive(false);
        StartCoroutine(LoadCounter(SceneName));
    }

    IEnumerator LoadCounter(string SceneName)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneName);
    }


    IEnumerator WaveTimer()
    {
        while (true)
        {
            StartCoroutine(StartWave());
            yield return new WaitForSeconds(Random.Range(0.5f, 4f));
        }
    }

    IEnumerator StartWave()
    {
        Vector2 GetPos;
        Vector2Int tmpVec;
        Debug.Log(TileManager.tileManager.ToGridVector(Center));
        int Range;
        do
        {
            GetPos = new Vector2(Center.x + Random.Range(-1f * Size.x, Size.x), Center.y + Random.Range(-1f * Size.y, Size.y));
            tmpVec = TileManager.tileManager.ToGridVector(GetPos);
            Range = Random.Range(2, 4);
        } while (ActivatingTile.ContainsKey(tmpVec) || !CheckDistance(tmpVec, Range));
        ActivatingTile.Add(tmpVec, Range);
        TileInteractScript.tileInteract.StartWave(tmpVec, Range);
        yield return new WaitForSeconds(Random.Range(2, 5));
        TileInteractScript.tileInteract.ReverseWave(tmpVec, Range);
        yield return new WaitForSeconds(1.5f);
        ActivatingTile.Remove(tmpVec);
    }

    bool CheckDistance(Vector2Int center, int Range)
    {
        if (ActivatingTile.Count > 0)
            foreach (var item in ActivatingTile)
            {
                int dis = TileManager.tileManager.GetDistance(item.Key, center);
                if (dis <= Range || dis <= item.Value)
                {
                    return false;
                }
            }
        return true;
    }
}

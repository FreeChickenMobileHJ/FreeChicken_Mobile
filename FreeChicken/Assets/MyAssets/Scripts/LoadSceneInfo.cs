using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneInfo : MonoBehaviour
{
    public static bool isStartScene; // 1
    public static bool is2DEnterScene; // 2

    public static bool isEndScene;   // 3


    public static int LevelCnt;
    public bool isChk;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    private void Start()
    {
        StartCoroutine(Set());
    }
    IEnumerator Set()
    {
        while (true)
        {
            if (LevelCnt == 1 && !isChk)
            {
                Invoke("LoadStart", 2.5f);
                isChk = true;
            }
            if (LevelCnt == 2 && !isChk)
            {
                Invoke("Load2D", 2.5f);
                isChk = true;
            }
            if (LevelCnt == 3 && !isChk)
            {
                Invoke("LoadEnd", 2.5f);
                isChk = true;
            }
            yield return null;
        }
    }
   

    void LoadStart()
    {
        SceneManager.LoadScene("Start");
    }
    void Load2D()
    {
        SceneManager.LoadScene("Enter2DScene");
    }
   
    void LoadEnd()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("EndingScene");
    }
   
}

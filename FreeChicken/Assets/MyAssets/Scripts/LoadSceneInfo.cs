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
    public void Load()
    {
        int SceneStart = PlayerPrefs.GetInt("SceneStart");
        if(SceneStart == 1)
        {
            isStartScene = true;
            LevelCnt = 1;
        }
        
        int Scene2D = PlayerPrefs.GetInt("Scene2D");
        if (Scene2D == 1)
        {
            is2DEnterScene = true;
            LevelCnt = 2;
        }
      
    }
    public void Update()
    {
        if (LevelCnt == 1 && !isChk)
        {
            Invoke("LoadStart", 2.5f);
            isChk = true;
        }
        if(LevelCnt == 2 && !isChk)
        {
            Invoke("Load2D", 2.5f);
            isChk=true;
        }
        if(LevelCnt == 3 && !isChk)
        {
            Invoke("LoadEnd", 2.5f);
            isChk = true;
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

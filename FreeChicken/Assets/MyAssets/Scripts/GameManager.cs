using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    FactoryPlayer factoryPlayer1;
    FactoryPlayer_2 factoryPlayer2;
    FactoryPlayer_3 factoryPlayer3;
    HouseScenePlayer housePlayer1;
    HouseScene2_Player housePlayer2;
    EvloutionPlayer evolutionPlayer;
    CityScenePlayer cityPlayer;
    CaveScenePlayer cavePlayer;


    public GameObject menuSet;
    public AudioSource ClickButtonAudio;
   
    
    public bool isStartScene;
    public bool isFactory_1;
    public bool isFactory_2;
    public bool isFactory_3;
    public bool isFactory_4;
    public bool isHouse_1;
    public bool isHouse_2;
    public bool isHouse_3;
    public bool isHouse_4;
    //public bool isHouse_5;

    public bool isHouse_5_Player2;
    public bool isHouse_5_EvoluPlayer;

    public bool isCity;
    public bool isCave_1;
    public bool isCave_2;
    public bool isCave_3;
    public bool isCave_4;
    public bool isCave_5;
    public bool isMain;

    public bool isLoading;
    public bool isStart;
    public bool isEnglish;

    public bool is2D;
    public bool isClick;

    public AudioSource MainBGM;
    public AudioSource SFX;
    public GameObject mainUI;
   
    public GameObject AudioSettingUI;
    public GameObject Control_UI;
    public GameObject WarnningUI;
    public GameObject ExitUI;
    public GameObject LoadingUI;
   
    public LocaleManager LocaleManager;
    string path;
    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "playerData.json");
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            factoryPlayer1 = GameObject.FindGameObjectWithTag("Player").GetComponent<FactoryPlayer>();
            factoryPlayer2 = GameObject.FindGameObjectWithTag("Player").GetComponent<FactoryPlayer_2>();
            factoryPlayer3 = GameObject.FindGameObjectWithTag("Player").GetComponent<FactoryPlayer_3>();
            housePlayer1 = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
            housePlayer2 = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScene2_Player>();
            evolutionPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<EvloutionPlayer>();
            cityPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CityScenePlayer>();
            cavePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        }
        
        if (File.Exists(path))
        {
   
            string jsonData = File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            isEnglish = loadedData.isEng;
            Debug.Log(isEnglish);
        }
        if (isEnglish)
        {
            if (LocaleManager != null)
            {
                PlayerData.isEnglish = true;
                LocaleManager = GetComponent<LocaleManager>();
                LocaleManager.ChangeLocale(0);
            }
        }
        else if (!isEnglish)
        {
            if (LocaleManager != null)
            {
                PlayerData.isEnglish = false;
                LocaleManager = GetComponent<LocaleManager>();
                LocaleManager.ChangeLocale(1);
            }
        }
    }
   

    public void PauseButton()
    {
        if (!isLoading && !isStart)
        {
            ClickButtonAudio.Play();
            if (factoryPlayer1 != null)
            {

                menuSet.SetActive(true);
                factoryPlayer1.mainAudio.Pause();
                factoryPlayer1.runAudio.Pause();
                Time.timeScale = 0f;
                factoryPlayer1.isTalk = true;
                //isFactory_1 = true;
            }
            else if (factoryPlayer2 != null)
            {
                menuSet.SetActive(true);
                factoryPlayer2.BGM.Pause();
                Time.timeScale = 0f;
                factoryPlayer2.isTalk = true;
                //isFactory_2 = true;
            }
            else if (factoryPlayer3 != null)
            {
                menuSet.SetActive(true);
                factoryPlayer3.BGM.Pause();
                Time.timeScale = 0f;
                factoryPlayer3.isTalk = true;
                //isFactory_3 = true;
            }
            else if (housePlayer1 != null)
            {
                menuSet.SetActive(true);
                housePlayer1.mainAudio.Pause();
                housePlayer1.runAudio.Pause();
                Time.timeScale = 0f;
                housePlayer1.isTalk = true;
                //isHouse_1 = true;
            }
            else if (housePlayer2 != null || evolutionPlayer!= null)
            {
                //menuSet.SetActive(true);
                if(isHouse_5_Player2)
                {
                    menuSet.SetActive(true);
                    housePlayer2.mainAudio.Pause();
                    housePlayer2.runAudio.Pause();
                    Time.timeScale = 0f;
                    housePlayer2.isTalk1 = true;
                    housePlayer2.isTalk2 = true;
                }
                else if(isHouse_5_EvoluPlayer)
                {
                    menuSet.SetActive(true);
                    evolutionPlayer.mainAudio.Pause();
                    evolutionPlayer.runAudio.Pause();
                    Time.timeScale = 0f;
                    evolutionPlayer.isTalk2 = true;
                }

                //isHouse_2 = true;
            }
            //else if (evolutionPlayer != null)
            //{
            //    menuSet.SetActive(true);
            //    evolutionPlayer.mainAudio.Pause();
            //    evolutionPlayer.runAudio.Pause();
            //    Time.timeScale = 0f;
            //    evolutionPlayer.isTalk2 = true;
            //    //isHouse_2 = true;
            //}
            else if (cityPlayer != null)
            {
                menuSet.SetActive(true);
                cityPlayer.BGM.Pause();
                cityPlayer.startAudio.Pause();
                Time.timeScale = 0f;
                cityPlayer.isAllStop = true;
                //isCity = true;
            }
            else if (cavePlayer != null)
            {
                menuSet.SetActive(true);
                cavePlayer.mainAudio.Pause();
                Time.timeScale = 0f;
                cavePlayer.isTalk = true;
                //isCave = true;
            }
            else if (isMain)
            {
                menuSet.SetActive(true);
                Time.timeScale = 0f;
                /*if (MainBGM != null)
                {
                    MainBGM.Pause();
                }*/

            }
        }
    }

    public void SetKorean()
    {
        PlayerData.isEnglish = false;
    }

    public void SetEnglish()
    {   
        PlayerData.isEnglish = true;
    }

    public void MainUIControlExit()
    {
          mainUI.SetActive(false);
    }

    public void ContinueGame()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1;

        if (isFactory_1 && factoryPlayer1 != null)
        {
            factoryPlayer1.mainAudio.UnPause();
            factoryPlayer1.runAudio.UnPause();
            factoryPlayer1.isTalk = false;
        }
        else if (isFactory_2 && factoryPlayer1 != null)
        {
            factoryPlayer1.mainAudio.UnPause();
            factoryPlayer1.isTalk = false;
        }
        else if (isFactory_3 && factoryPlayer2 != null)
        {
            factoryPlayer2.BGM.UnPause();
            factoryPlayer2.isTalk = false;
        }
        else if (isFactory_4 && factoryPlayer3 != null)
        {
            factoryPlayer3.BGM.UnPause();
            factoryPlayer3.isTalk = false;
        }
        else if(isCity && cityPlayer != null)
        {
            cityPlayer.BGM.UnPause();
            cityPlayer.isAllStop = false;

        }
        else if (isHouse_1 && housePlayer1 != null)
        {
            housePlayer1.mainAudio.UnPause();
            housePlayer1.runAudio.UnPause();
            housePlayer1.isTalk = false;
        }
        else if (isHouse_2 && housePlayer1 != null)
        {
            housePlayer1.mainAudio.UnPause();
            housePlayer1.runAudio.UnPause();
            housePlayer1.isTalk = false;
        }
        else if (isHouse_3 && housePlayer1 != null)
        {
            housePlayer1.mainAudio.UnPause();
            housePlayer1.runAudio.UnPause();
            housePlayer1.isTalk = false;
        }
        else if (isHouse_4 && housePlayer2 != null)
        {
            housePlayer2.mainAudio.UnPause();
            housePlayer2.runAudio.UnPause();
            housePlayer2.isTalk2 = false;
        }
        else if (isHouse_5_Player2 || isHouse_5_EvoluPlayer)
        {
            if (evolutionPlayer != null) {
                evolutionPlayer.mainAudio.UnPause();
                evolutionPlayer.runAudio.UnPause();
                evolutionPlayer.isTalk2 = false;
            }
            else if (housePlayer2 != null)
            {
                housePlayer2.mainAudio.UnPause();
                housePlayer2.runAudio.UnPause();
                housePlayer2.isTalk2 = false;
            }
        }
        else if (isCity && cityPlayer != null)
        {
            cityPlayer.BGM.UnPause();
            cityPlayer.startAudio.UnPause();
            cityPlayer.isAllStop = false;
        }
        else if (isCave_1 && cavePlayer != null)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isTalk = false;
        }
        else if (isCave_2 && cavePlayer != null)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isTalk = false;
        }
        else if (isCave_3 && cavePlayer != null)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isTalk = false;
        }
        else if (isCave_4 && cavePlayer != null)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isTalk = false;
        }
        else if (isCave_5 && cavePlayer != null)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isTalk = false;
        }
        else if (isMain)
        {
            if (MainBGM != null)
            {
                MainBGM.UnPause();
            }
        }
    }
  
    public void GameExit()
    {
        Application.Quit();
    }

    public void Enter()
    {
        if (isFactory_1)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isFactory_2)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isFactory_3)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isFactory_4)
        {
            SceneManager.LoadScene("Enter2DScene");
        }

        else if (isHouse_1)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isHouse_2)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isHouse_3)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isHouse_4)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isHouse_5_Player2 || isHouse_5_EvoluPlayer)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isCity)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isCave_1)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isCave_2)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isCave_3)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isCave_4)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
        else if (isCave_5)
        {
            SceneManager.LoadScene("Enter2DScene");
        }
    }
    public void Enter2dScene()
    {

        Time.timeScale = 1f;
        MemoryCount.memCount = 0;
        if (isFactory_1)
        {
            GameSave.Level = 1;
          

        }
        else if (isFactory_2)
        {
            GameSave.Level = 2;
        }
        else if (isFactory_3)
        {
            GameSave.Level = 3;
        }
        else if (isFactory_4)
        {
            GameSave.Level = 4;
        }
        else if (isHouse_1)
        {
            GameSave.Level = 5;
        }
        else if (isHouse_2)
        {
            GameSave.Level = 6;
        }
        else if (isHouse_3)
        {
            GameSave.Level = 7;
        }
        else if (isHouse_4)
        {
            GameSave.Level = 8;
        }
        else if (isHouse_5_Player2 || isHouse_5_EvoluPlayer)
        {
            GameSave.Level = 9;
        }
        else if (isCity)
        {
            GameSave.Level = 10;
        }
        else if (isCave_1)
        {
            GameSave.Level = 11;
        }
        else if (isCave_2)
        {
            GameSave.Level = 12;
        }
        else if (isCave_3)
        {
            GameSave.Level = 13;
        }
        else if (isCave_4)
        {
            GameSave.Level = 14;
        }
        else if (isCave_5)
        {
            GameSave.Level = 15;
        }
        SetAndGo(GameSave.Level);
    }
    public void SetAndGo(int num)
    {
        if (File.Exists(path))
        {
            
            string jsonData = File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            if (loadedData.LevelChk >= num)
            {
                GameSave.Level = loadedData.LevelChk;
            }
            else
            {
                GameSave.Level = num;
            }
        }
        else
        {
            GameSave.Level = num;
        }


        LoadSceneInfo.is2DEnterScene = true;
        PlayerPrefs.SetInt("Scene2DEnter", LoadSceneInfo.is2DEnterScene? 1 : 0);
        LoadSceneInfo.LevelCnt = 2;
        SceneManager.LoadScene("LoadingScene");
    }
    public void AudioSettingScene()
    {
        AudioSettingUI.SetActive(true);
    }

    public void StartScene1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene_Final");
    }
    public void StartScene2()
    {
        
        Invoke("StartRealScene2", 0.35f);
        
    }
    public void StartRealScene2()
    {
       
        if (File.Exists(path))
        {
          

            string jsonData = File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

                
            GameSave.Level = loadedData.LevelChk;
            LoadSceneInfo.is2DEnterScene = true;
            PlayerPrefs.SetInt("Scene2D", LoadSceneInfo.is2DEnterScene ? 1 : 0);
            LoadSceneInfo.LevelCnt = 2;

            SceneManager.LoadScene("LoadingScene");
         
        }
        else
        {
            if (isEnglish)
            {
                if (LocaleManager != null)
                {
                    LocaleManager = GetComponent<LocaleManager>();
                    LocaleManager.ChangeLocale(0);
                }
            }
            else if (!isEnglish)
            {
                if (LocaleManager != null)
                {
                    LocaleManager = GetComponent<LocaleManager>();
                    LocaleManager.ChangeLocale(1);
                }
            }
            LoadSceneInfo.isStartScene = true;
            PlayerPrefs.SetInt("SceneStart", LoadSceneInfo.isStartScene ? 1 : 0);
            LoadSceneInfo.LevelCnt = 1;
            SceneManager.LoadScene("LoadingScene");
        }
    }
    public void Enter2DExit()
    {
        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;
       
        if (PlayerData.isEnglish)
        {
            playerData.isEng = true;
        }
        else if(!PlayerData.isEnglish)   
        {
            playerData.isEng = false;
        }
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json); 
    }
    public void ReSetEveryThing()
    {
        isEnglish = true;

        if (LocaleManager != null)
        {
            LocaleManager = GetComponent<LocaleManager>();
            LocaleManager.ChangeLocale(0);
            PlayerData.isEnglish = true;
        }
        GameSave.Level = 0;
        DeadCount.count = 0;

        if (File.Exists(Application.persistentDataPath + "/playerData.json"))
        {

            string jsonData = File.ReadAllText(Application.persistentDataPath + "/playerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            loadedData.LevelChk = 0;

        }
        File.Delete(Application.persistentDataPath + "/playerData.json");
        
      
    }
   
    public void Controls()
    {
        Control_UI.SetActive(true);
    }
    public void Warnning()
    {
        if(WarnningUI != null) WarnningUI.SetActive(true);
    }
    public void WarnningExit()
    {
        WarnningUI.SetActive(false);
    }
    public void ControlsExit()
    {
        Control_UI.SetActive(false);
    }
  
    public void ExitShow()
    {
        if (ExitUI != null) ExitUI.SetActive(true);
       
    }
    public void ExitEnd()
    {
        ExitUI.SetActive(false);
    }
    public void ReplayGame()
    {
        Time.timeScale = 1f;
        if (isFactory_1)
        {
            MemoryCount.memCount = 0;
            SceneManager.LoadScene("FactoryScene_1");
        }
        else if (isFactory_2)
        {

            MemoryCount.memCount = 1;
            
            SceneManager.LoadScene("FactoryScene_2");
        }
        else if (isFactory_3)
        {

            MemoryCount.memCount = 2;
            
            SceneManager.LoadScene("FactoryScene_3");
        }
        else if (isFactory_4)
        {

            MemoryCount.memCount = 3;
            
            SceneManager.LoadScene("FactoryScene_4");
        }
        else if (isHouse_1)
        {
            MemoryCount.memCount = 0;
            SceneManager.LoadScene("HouseScene1");
        }
        else if (isHouse_2)
        {
            MemoryCount.memCount = 1;
            SceneManager.LoadScene("HouseScene2");
        }
        else if (isHouse_3)
        {
            MemoryCount.memCount = 2;
            SceneManager.LoadScene("HouseScene3");
        }
        else if (isHouse_4)
        {

            SceneManager.LoadScene("HouseScene4");
        }
        else if (isHouse_5_Player2 || isHouse_5_EvoluPlayer)
        {
            SceneManager.LoadScene("HouseScene5");
        }
        else if (isCity)
        {
            SceneManager.LoadScene("CityScene");
        }
        else if (isCave_1)
        {
            SceneManager.LoadScene("CaveScene_1");
        }
        else if (isCave_2)
        {
            SceneManager.LoadScene("CaveScene_2");
        }
        else if (isCave_3)
        {
            SceneManager.LoadScene("CaveScene_3");
        }
        else if (isCave_4)
        {
            SceneManager.LoadScene("CaveScene_4");
        }
        else if (isCave_5)
        {
            SceneManager.LoadScene("CaveScene_5");
        }
    }

    public void ControlsUI()
    {
        if (isMain)
        {
            if(mainUI != null)
            {
                mainUI.gameObject.SetActive(true);
            }
        }
    }

    public void ClickButtonSound()
    {
        ClickButtonAudio.Play();
    }
}

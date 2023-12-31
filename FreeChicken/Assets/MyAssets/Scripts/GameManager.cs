using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    FactoryPlayer factoryPlayer1;
    FactoryPlayer_2 factoryPlayer2;
    FactoryPlayer_3 factoryPlayer3;
    HouseScenePlayer housePlayer1;
    HouseScene2_Player housePlayer2;
    EvloutionPlayer evolutionPlayer;
    CityScenePlayer cityPlayer;
    CaveScenePlayer cavePlayer;

    [Header("Bool")]
    public bool isStartScene;
    public bool isFactory_1;
    public bool isFactory_2;
    public bool isFactory_3;
    public bool isFactory_4;
    public bool isHouse_1;
    public bool isHouse_2;
    public bool isHouse_3;
    public bool isHouse_4;
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

    [Header("GameObjects")]
    public GameObject menuSet;
    public AudioSource ClickButtonAudio;
    public AudioSource MainBGM;
    public AudioSource SFX;
    public GameObject mainUI;
    public GameObject AudioSettingUI;
    public GameObject Control_UI;
    public GameObject WarnningUI;
    public GameObject ExitUI;
    public GameObject enter2D_UI;
    public GameObject startScene_UI;
  
    public LocaleManager LocaleManager;
    string path;
    public MemoryCount memCnt;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    void Start()
    {
        path = Path.Combine(Application.persistentDataPath + "/playerData.json");
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
            //cavePlayer = GameObject.Find("CaveCharacter").GetComponent<CaveScenePlayer>();
        }
        if (memCnt != null)
        {
            memCnt = memCnt.GetComponent<MemoryCount>();
        }
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            isEnglish = loadedData.isEng;
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
                factoryPlayer1.isUnActive = true;
                //isFactory_1 = true;
            }
            else if (factoryPlayer2 != null)
            {
                menuSet.SetActive(true);
                factoryPlayer2.BGM.Pause();
                Time.timeScale = 0f;
                factoryPlayer2.isUnActive = true;
                //isFactory_2 = true;
            }
            else if (factoryPlayer3 != null)
            {
                menuSet.SetActive(true);
                factoryPlayer3.BGM.Pause();
                Time.timeScale = 0f;
                factoryPlayer3.isUnActive = true;
                //isFactory_3 = true;
            }
            else if (housePlayer1 != null)
            {
                menuSet.SetActive(true);
                housePlayer1.mainAudio.Pause();
                housePlayer1.runAudio.Pause();
                Time.timeScale = 0f;
                housePlayer1.isUnActive = true;
            }
            else if (housePlayer2 != null || evolutionPlayer != null)
            {
                if (isHouse_4)
                {
                    menuSet.SetActive(true);
                    housePlayer2.mainAudio.Pause();
                    housePlayer2.runAudio.Pause();
                    Time.timeScale = 0f;
                    housePlayer2.isUnActive = true;
                }
                else if (isHouse_5_Player2)
                {
                    menuSet.SetActive(true);
                    housePlayer2.mainAudio.Pause();
                    housePlayer2.runAudio.Pause();
                    Time.timeScale = 0f;
                    housePlayer2.isUnActive = true;
                }
                else if (isHouse_5_EvoluPlayer)
                {
                    menuSet.SetActive(true);
                    evolutionPlayer.mainAudio.Pause();
                    evolutionPlayer.runAudio.Pause();
                    Time.timeScale = 0f;
                    evolutionPlayer.isUnActive = true;
                }
            }
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
                cavePlayer.isUnActive = true;
            }
            else if (isMain)
            {
                menuSet.SetActive(true);
                Time.timeScale = 0f;
                enter2D_UI.SetActive(false);
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
            factoryPlayer1.isUnActive = false;
        }
        else if (isFactory_2 && factoryPlayer1 != null)
        {
            factoryPlayer1.mainAudio.UnPause();
            factoryPlayer1.isUnActive = false;
        }
        else if (isFactory_3 && factoryPlayer2 != null)
        {
            factoryPlayer2.BGM.UnPause();
            factoryPlayer2.isUnActive = false;
        }
        else if (isFactory_4 && factoryPlayer3 != null)
        {
            factoryPlayer3.BGM.UnPause();
            factoryPlayer3.isUnActive = false;
        }
        else if (isCity && cityPlayer != null)
        {
            cityPlayer.BGM.UnPause();
            cityPlayer.isAllStop = false;

        }
        else if (isHouse_1 && housePlayer1 != null)
        {
            housePlayer1.mainAudio.UnPause();
            housePlayer1.runAudio.UnPause();
            housePlayer1.isUnActive = false;
        }
        else if (isHouse_2 && housePlayer1 != null)
        {
            housePlayer1.mainAudio.UnPause();
            housePlayer1.runAudio.UnPause();
            housePlayer1.isUnActive = false;
        }
        else if (isHouse_3 && housePlayer1 != null)
        {
            housePlayer1.mainAudio.UnPause();
            housePlayer1.runAudio.UnPause();
            housePlayer1.isUnActive = false;
        }
        else if (isHouse_4 && housePlayer2 != null)
        {
            housePlayer2.mainAudio.UnPause();
            housePlayer2.runAudio.UnPause();
            housePlayer2.isUnActive = false;
        }
        else if (isHouse_5_Player2 || isHouse_5_EvoluPlayer)
        {
            if (evolutionPlayer != null)
            {
                evolutionPlayer.mainAudio.UnPause();
                evolutionPlayer.runAudio.UnPause();
                evolutionPlayer.isUnActive = false;
            }
            else if (housePlayer2 != null)
            {
                housePlayer2.mainAudio.UnPause();
                housePlayer2.runAudio.UnPause();
                housePlayer2.isUnActive = false;
            }
        }
        else if (isCave_1 && cavePlayer != null)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isUnActive = false;
        }
        else if (isCave_2 && cavePlayer != null)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isUnActive = false;
        }
        else if (isCave_3 && cavePlayer != null)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isUnActive = false;
        }
        else if (isCave_4 && cavePlayer != null)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isUnActive = false;
        }
        else if (isCave_5 && cavePlayer != null && !cavePlayer.MomContacting)
        {
            cavePlayer.mainAudio.UnPause();
            cavePlayer.isUnActive = false;
        }
        else if (isMain)
        {
            if (MainBGM != null)
            {
                MainBGM.UnPause();
                enter2D_UI.SetActive(true);
            }
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void Enter()
    {
        LoadingSceneManager.LoadScene("Enter2DScene");
    }
    public void Enter2dScene()
    {
        Time.timeScale = 1f;

        if (isFactory_1)
        {
            GameSave.Level = 1;
            memCnt.MemCntChange(0, 0);
        }
        else if (isFactory_2)
        {
            GameSave.Level = 2;
            memCnt.MemCntChange(0, 0);
        }
        else if (isFactory_3)
        {
            GameSave.Level = 3;
            memCnt.MemCntChange(0, 0);
        }
        else if (isFactory_4)
        {
            GameSave.Level = 4;
            memCnt.MemCntChange(0, 0);
        }
        else if (isHouse_1)
        {
            GameSave.Level = 5;
            memCnt.MemCntChange(0, 0);
        }
        else if (isHouse_2)
        {
            GameSave.Level = 6;
            memCnt.MemCntChange(0, 0);
        }
        else if (isHouse_3)
        {
            GameSave.Level = 7;
            memCnt.MemCntChange(0, 0);
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
        LoadingSceneManager.LoadScene("Enter2DScene");
    }
   
    public void AudioSettingScene()
    {
        AudioSettingUI.SetActive(true);
    }

    public void StartScene1()
    {
        Time.timeScale = 1f;
        LoadingSceneManager.LoadScene("StartScene_Final");
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
          
            LoadingSceneManager.LoadScene("Enter2DScene");
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
            LoadingSceneManager.LoadScene("StartSceneShow");
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
        else if (!PlayerData.isEnglish)
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
        startScene_UI.SetActive(false);
    }

    public void Warnning()
    {
        if (WarnningUI != null)
        {
            WarnningUI.SetActive(true);
            startScene_UI.SetActive(false);
        }
    }

    public void WarnningExit()
    {
        WarnningUI.SetActive(false);
        startScene_UI.SetActive(true);
    }

    public void ControlsExit()
    {
        Control_UI.SetActive(false);
        startScene_UI.SetActive(true);
    }

    public void ExitShow()
    {
        if (ExitUI != null)
        {
            ExitUI.SetActive(true);
            startScene_UI.SetActive(false);
        }
    }
    public void ExitEnd()
    {
        ExitUI.SetActive(false);
        startScene_UI.SetActive(true);
    }
    public void ReplayGame()
    {
        Time.timeScale = 1f;
        if (isFactory_1)
        {
            memCnt.MemCntChange(0, 1);
            LoadingSceneManager.LoadScene("FactoryScene_1");
        }
        else if (isFactory_2)
        {
            memCnt.MemCntChange(1, 2);
            LoadingSceneManager.LoadScene("FactoryScene_2");
        }
        else if (isFactory_3)
        {
            memCnt.MemCntChange(2, 3);
            LoadingSceneManager.LoadScene("FactoryScene_3");
        }
        else if (isFactory_4)
        {
            memCnt.MemCntChange(3, 4);
            LoadingSceneManager.LoadScene("FactoryScene_4");
        }
        else if (isHouse_1)
        {
            memCnt.MemCntChange(0, 1);
            LoadingSceneManager.LoadScene("HouseScene1");
        }
        else if (isHouse_2)
        {
            memCnt.MemCntChange(1, 2);
            LoadingSceneManager.LoadScene("HouseScene2");
        }
        else if (isHouse_3)
        {
            memCnt.MemCntChange(2, 3);
            LoadingSceneManager.LoadScene("HouseScene3");
        }
        else if (isHouse_4)
        {
            LoadingSceneManager.LoadScene("HouseScene4");
        }
        else if (isHouse_5_Player2 || isHouse_5_EvoluPlayer)
        {
            LoadingSceneManager.LoadScene("HouseScene5");
        }
        else if (isCity)
        {
            LoadingSceneManager.LoadScene("CityScene");
        }
        else if (isCave_1)
        {
            LoadingSceneManager.LoadScene("CaveScene_1");
        }
        else if (isCave_2)
        {
            LoadingSceneManager.LoadScene("CaveScene_2");
        }
        else if (isCave_3)
        {
            LoadingSceneManager.LoadScene("CaveScene_3");
        }
        else if (isCave_4)
        {
            LoadingSceneManager.LoadScene("CaveScene_4");
        }
        else if (isCave_5)
        {
            LoadingSceneManager.LoadScene("CaveScene_5");
        }
    }

    public void ControlsUI()
    {
        if (isMain)
        {
            if (mainUI != null)
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
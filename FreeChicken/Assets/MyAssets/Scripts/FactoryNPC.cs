using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System.IO;
using UnityEngine.SceneManagement;
public class FactoryNPC : MonoBehaviour
{
    public Slider NpcUI;
    public GameObject factoryUI;
    public GameObject GetMemoryUI;
    public FactoryPlayer player;
    public FactoryPlayer_2 player_2;
    public FactoryPlayer_3 player_3;
    public HouseScenePlayer player_4;
    
    public bool isEbutton;
    public GameObject Ebutton;
    public TextMeshProUGUI E;

    public GameObject EnglishUI;
    public GameObject KoreanUI;
   
    public bool isNear;
    public static bool isFinish;
    public bool isSet;
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera npcCam;
    public GameObject npc;
    public GameObject CamImage;
    public AudioSource getMemorySound;

    public bool isFactory_2;
    public float t;
    void Start()
    {
        
        Ebutton.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer>();
        player_2 = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer_2>();
        player_3 = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer_3>();
        
        t = 0;
       
    }
  
 
    public void Set()
    {
        CamImage.SetActive(true);
        isEbutton = false;
        npc.SetActive(false);

        if (player != null)
        {
            player.isStopSlide = true;
            player.isSlide = false;
            player.isTalk = true;
        }
        else if (player_2 != null)
        {
            player_2.isStopSlide = true;
            player_2.isSlide = false;
            player_2.isTalk = true;
        }
        else if (player_3 != null)
        {
            player_3.isStopSlide = true;
            player_3.isSlide = false;
            player_3.isTalk = true;

        }

        getMemorySound.Play();
        GetMemoryUI.SetActive(true);

        Ebutton.SetActive(false);

        MemoryCount.memCount++;

        Invoke("ReStart", 2f);

    }
    public void ReStart()
    {
        if(player != null && !isFactory_2)
        {
            /*GameSave.isFactory_2 = true;

            PlayerPrefs.SetInt("GoFactory_2", GameSave.isFactory_2 ? 1 : 0);
*/
            GameSave.Level = 2;
            if (File.Exists("PlayerData.json"))
            {
                
                string jsonData = File.ReadAllText("playerData.json");
                PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

                if (loadedData.LevelChk >= GameSave.Level)
                {
                    GameSave.Level = loadedData.LevelChk;
                }
                else
                {
                    GameSave.Level = 2;
                }
            }
            else
            {
                GameSave.Level = 2;
            }



            //playerData.isEnglish = true;

            LoadSceneInfo.is2DEnterScene = true;
            PlayerPrefs.SetInt("Scene2D", LoadSceneInfo.is2DEnterScene ? 1 : 0);
            LoadSceneInfo.LevelCnt = 2;
            SceneManager.LoadScene("LoadingScene");
        }
        if(player != null && isFactory_2)
        {
            /* GameSave.isFactory_3 = true;

             PlayerPrefs.SetInt("GoFactory_3", GameSave.isFactory_3 ? 1 : 0);*/

            GameSave.Level = 3;

            if (File.Exists("PlayerData.json"))
            {
                
                string jsonData = File.ReadAllText("playerData.json");
                PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

                if (loadedData.LevelChk >= GameSave.Level)
                {
                    GameSave.Level = loadedData.LevelChk;
                }
                else
                {
                    GameSave.Level = 3;
                }
            }
            else
            {
                GameSave.Level = 3;
            }

           
           
            LoadSceneInfo.is2DEnterScene = true;
            PlayerPrefs.SetInt("Scene2D", LoadSceneInfo.is2DEnterScene ? 1 : 0);
            LoadSceneInfo.LevelCnt = 2;
            SceneManager.LoadScene("LoadingScene");
        }
        CamImage.SetActive(false);
        GetMemoryUI.SetActive(false);
        isFinish = true;
       
        this.gameObject.SetActive(false);
       
        if (factoryUI != null)
        {
            factoryUI.gameObject.SetActive(true);
            if (PlayerData.isEnglish )
            {
                EnglishUI.SetActive(true);
            }
            else if (!PlayerData.isEnglish)
            {
                KoreanUI.SetActive(true);
            }
        }
        else if(factoryUI == null)
        {
            isNear = false;
            isEbutton = false;
            Ebutton.SetActive(false);
            mainCam.Priority = 2;
            npcCam.Priority = 1;
            if (player != null)
            {
                player.isTalk = false;
            }
            else if (player_2 != null)
            {
                player_2.isTalk = false;
            }
            else if(player_3 != null)
            {
                player_3.isTalk = false;
                
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isNear = true;
            isEbutton = true;

            Ebutton.SetActive(true);
            mainCam.Priority = 1;
            npcCam.Priority = 2;
        }
    }
   
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            isEbutton = false;
            
            Ebutton.SetActive(false);
            mainCam.Priority = 2;
            npcCam.Priority = 1;
            
        }
    }

}

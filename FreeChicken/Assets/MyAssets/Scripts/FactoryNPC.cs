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
   
   
    public GameObject GetMemoryUI;
    public FactoryPlayer player;
    public FactoryPlayer_2 player_2;
    public FactoryPlayer_3 player_3;
    public HouseScenePlayer player_4;
    
    public bool isEbutton;
    public GameObject Ebutton;
    public TextMeshProUGUI E;

   
   
    public bool isNear;
    public static bool isFinish;
    public bool isSet;
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera npcCam;
    public GameObject npc;
    public GameObject CamImage;
    public AudioSource getMemorySound;

    public bool isFactory_2;
    public bool isFactory_3;
    public bool isF_3;
    
    public MemoryCount memCnt;
    public int cnt;
  
    void Start()
    {
        
        Ebutton.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer>();
        player_2 = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer_2>();
        player_3 = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer_3>();
        memCnt = memCnt.GetComponent<MemoryCount>();

       
      
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
            cnt = 1;

        }
        else if (player_2 != null)
        {
            player_2.isStopSlide = true;
            player_2.isSlide = false;
            player_2.isTalk = true;
            cnt = 2;
            if (isF_3)
            {
                cnt = 3;
            }
        }
        else if (player_3 != null)
        {
            player_3.isStopSlide = true;
            player_3.isSlide = false;
            player_3.isTalk = true;

            cnt = 4;
        
        }

        getMemorySound.Play();
        GetMemoryUI.SetActive(true);

        Ebutton.SetActive(false);

      
        memCnt.MemCntChange(cnt,cnt);
        Invoke("ReStart", 2f);

    }
    public void ReStart()
    {
        if(player != null && !isFactory_2)
        {
           
            GameSave.Level = 2;           
            LoadingSceneManager.LoadScene("Enter2DScene"); // 12.15 ¼öÁ¤¿Ï
        }
        if(player != null && isFactory_2)
        {
           
            GameSave.Level = 3;
            LoadingSceneManager.LoadScene("Enter2DScene");
        }
        CamImage.SetActive(false);
        GetMemoryUI.SetActive(false);
        isFinish = true;
       
        this.gameObject.SetActive(false);
       
     
        if(isFactory_3)
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

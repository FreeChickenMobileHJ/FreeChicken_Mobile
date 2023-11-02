using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameSave : MonoBehaviour
{
    public bool isChk;
    public static bool isFactory_2;
    public static bool isFactory_3;
    public static bool isFactory_4;
    public static bool isHouse_1;
    public static bool isHouse_2;
    public static bool isHouse_3;
    public static bool isHouse_4;
    public static bool isHouse_5;
    public static bool isCity;
    public static bool isCave_1;
    public static bool isCave_2;
    public static bool isCave_3;
    public static bool isCave_4;

    [Header("GameObject")]
    public GameObject Factory_2; // 2
    public GameObject Factory_3; // 3
    public GameObject Factory_4; // 4

    public GameObject House_1;   // 5
    public GameObject House_2;   // 6
    public GameObject House_3;   // 7
    public GameObject House_4;   // 8
    public GameObject House_5;   // 9 

    public GameObject City;      // 10 
    public GameObject Cave_1;    // 11
    public GameObject Cave_2;    // 12
    public GameObject Cave_3;    // 13
    public GameObject Cave_4;    // 14
    public GameObject Cave_5;

    public GameObject[] Objects;
    public AudioSource ShowSound;

    [Header("Particle")]
    public ParticleSystem ShowParticle_Factory_2;
    public ParticleSystem ShowParticle_Factory_3;
    public ParticleSystem ShowParticle_Factory_4;
    public ParticleSystem ShowParticle_House_1;
    public ParticleSystem ShowParticle_House_2;
    public ParticleSystem ShowParticle_House_3;
    public ParticleSystem ShowParticle_House_4;
    public ParticleSystem ShowParticle_House_5;
    public ParticleSystem ShowParticle_City;

    public ParticleSystem ShowParticle_Cave_1;
    public ParticleSystem ShowParticle_Cave_2;
    public ParticleSystem ShowParticle_Cave_3;
    public ParticleSystem ShowParticle_Cave_4;
    public ParticleSystem ShowParticle_Cave_5;


    public static int Level;
    public bool isExist;
  
    private void Awake()
    {

        Application.targetFrameRate = 30;
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {

            string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);


            Level = loadedData.LevelChk;

        }
        for (int i = 1; i < Level; i++)
        {
            Objects[i].SetActive(true);
        }
      

    }

    public void Start()
    {

        if (Level == 2 && !isChk)
        {
            Factory_2.SetActive(true);
            ShowSound.Play();

            ShowParticle_Factory_2.Play();
            SetFile();
            isChk = true;
        }
        if (Level == 3 && !isChk)
        {

            Factory_3.SetActive(true);
            ShowSound.Play();

            ShowParticle_Factory_3.Play();
            SetFile();
            isChk = true;
        }
        if (Level == 4 && !isChk)
        {

            Factory_4.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_Factory_4.Play();

            isChk = true;
        }
        if (Level == 5 && !isChk)
        {

            House_1.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_House_1.Play();

            isChk = true;
        }
        if (Level == 6 && !isChk)
        {

            House_2.SetActive(true);
            ShowSound.Play();

            ShowParticle_House_2.Play();
            SetFile();
            isChk = true;
        }
        if (Level == 7 && !isChk)
        {

            House_3.SetActive(true);
            ShowSound.Play();

            ShowParticle_House_3.Play();
            SetFile();
            isChk = true;
        }
        if (Level == 8 && !isChk)
        {

            House_4.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_House_4.Play();

            isChk = true;
        }
        if (Level == 9 && !isChk)
        {

            House_5.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_House_5.Play();

            isChk = true;
        }
        if (Level == 10 && !isChk)
        {

            City.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_City.Play();

            isChk = true;
        }
        if (Level == 11 && !isChk)
        {

            Cave_1.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_Cave_1.Play();

            isChk = true;
        }
        if (Level == 12 && !isChk)
        {

            Cave_2.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_Cave_2.Play();

            isChk = true;
        }
        if (Level == 13 && !isChk)
        {

            Cave_3.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_Cave_3.Play();

            isChk = true;
        }
        if (Level == 14 && !isChk)
        {

            Cave_4.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_Cave_4.Play();

            isChk = true;
        }
        if (Level == 15 && !isChk)
        {

            Cave_5.SetActive(true);
            ShowSound.Play();
            SetFile();
            ShowParticle_Cave_5.Play();

            isChk = true;
        }
    }
    public void SetFile()
    {
        PlayerData playerData = new PlayerData();
        playerData.LevelChk = Level;

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
}
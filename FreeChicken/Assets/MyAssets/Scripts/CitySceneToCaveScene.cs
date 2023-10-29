using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.IO;
public class CitySceneToCaveScene : MonoBehaviour
{
    
    public GameObject pos;
    public CityScenePlayer player;
    public bool isContact;
    public bool isMove;
  
    public CinemachineVirtualCamera endCam;
    public AudioSource CarSound;

    
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CityScenePlayer>();
        StartCoroutine(Go());
    }
    IEnumerator Go()
    {
        while (true)
        {
            if (player.isLast)
            {

                if (isContact)
                {

                    endCam.Priority = 2;
                    CarSound.Play();
                    player.gameObject.transform.position = pos.transform.position;
                    player.anim_2.SetBool("isRun", false);
                    this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 4f, Space.World);

                    Invoke("LoadCaveScene", 3f);


                }
            }
            yield return null;
        }
    }
   
    void LoadCaveScene()
    {

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            GameSave.Level = 11;
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            if (loadedData.LevelChk >= GameSave.Level)
            {
                GameSave.Level = loadedData.LevelChk;
            }
            else
            {
                GameSave.Level = 11;
            }
        }
        else
        {
            GameSave.Level = 11;
        }

        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;


        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
        LoadSceneInfo.LevelCnt = 2;

        SceneManager.LoadScene("LoadingScene");

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isContact = true;
        }
    }
}

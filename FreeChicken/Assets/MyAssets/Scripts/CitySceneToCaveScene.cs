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
        GameSave.Level = 11;      
        LoadingSceneManager.LoadScene("Enter2DScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {         
            isContact = true;
            CarSound.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class FactorySceneChangeZone : MonoBehaviour
{
    [Header("Stats")]
    public GameObject ChangeConveorZone;
    public GameObject ChangeFinish;
    public GameObject Player;
    public ParticleSystem Particle;
    public AudioSource ParticleSound;

    public GameObject zoneL;
    public GameObject zoneR;
    public GameObject zoneG;
    public float t;

    public GameObject BigEgg;
    public GameObject Pos;

    public AudioSource ClickSound;

    public TextMeshProUGUI Rtxt;
    public TextMeshProUGUI Etxt;

    public ObjectPool objectPool;
    [Header("Bool")]
    public bool isButton;
    public bool isL;
    public bool isR;
    public bool isG;
   
    public bool isScene_2;
    public bool isChk;
    public bool isEnd;
 
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera ChangeCam;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");   
    }
    void Update()
    {
       
        if(Player.GetComponent<FactoryPlayer_2>().isDie)
        {

            StartCoroutine("Reset");
          
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.7f);
        zoneL.gameObject.SetActive(false);
        zoneR.gameObject.SetActive(false);
        zoneG.gameObject.SetActive(false);
       
        t = 0;
        isEnd = false;
        ChangeConveorZone.SetActive(false);
        isButton = false;
        isChk = false;
        ChangeCam.Priority = 1;
        mainCam.Priority = 3;
    }
    public void Chk()
    {
       
        if (!isL && !isG && !isR)
        {
            Rtxt.color = Color.red;
            Particle.Play();
            ClickSound.Play();
            
            zoneR.gameObject.SetActive(false);
            zoneL.gameObject.SetActive(true);
            isL = true;
            isR = false;
            isG = false;
        }
       
        else if (!isG && isL && !isR)
        {
            Rtxt.color = Color.red;
            Particle.Play();
            ClickSound.Play();
            zoneL.gameObject.SetActive(false);
            zoneG.gameObject.SetActive(true);
            isL = false;
            isG = true;
            isR = false;
        }
        
        else if (!isR && !isL && isG)
        {
            Rtxt.color = Color.red;
            Particle.Play();
            ClickSound.Play();
            zoneG.gameObject.SetActive(false);
            zoneR.gameObject.SetActive(true);
            isG = false;
            isR = true;
            isL = false;
            
        } 
        isR = false;
        Rtxt.color = Color.yellow;


    }
    public void SetRoad()
    {


        ParticleSound.Play();
        ChangeConveorZone.gameObject.SetActive(false);
        ChangeFinish.gameObject.SetActive(true);
        isButton = false;


        Player.GetComponent<FactoryPlayer_2>().isSlide = true;


        StartCoroutine(TheEnd());


    }
    IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(1f);
        ChangeCam.Priority = 1;
        mainCam.Priority = 3;
        ChangeFinish.gameObject.SetActive(false);
        isChk = false;
        isEnd = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isChk && !isEnd)
        {
            isChk = true;
            ChangeConveorZone.SetActive(true);
            isButton = true;
            ChangeCam.Priority = 3;
            mainCam.Priority = 1;
            StartCoroutine(SpawnBigEgg());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isChk = false;
            ChangeConveorZone.SetActive(false);
            isButton = false;
            ChangeCam.Priority = 1;
            mainCam.Priority = 3;
        }
    }
    IEnumerator SpawnBigEgg()
    {
        yield return new WaitForSeconds(2f);
        GameObject obj = objectPool.GetObjectFromPool(Pos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        objectPool.ReturnObjectToPool(obj);

    }
   
}

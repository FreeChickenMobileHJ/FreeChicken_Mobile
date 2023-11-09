using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class FactoryNPC_1 : MonoBehaviour
{
    public HouseScene2_Player player;

    public bool isEbutton;
    public bool isClickbutton;
    public GameObject Ebutton;

    public bool isNear;

    public CinemachineVirtualCamera npccam;
    public CinemachineVirtualCamera maincam;

    public GameObject npc;
    public GameObject Video;

    public AudioSource BGM;
    public AudioSource Memory;
    public GameObject TalkUI1;
    public GameObject TalkUI2;
    public bool isFin;
    public GameObject Wall;
    public GameManager gameManager;
    private bool hasPlayedBGM = false;

    void Start()
    {
        Ebutton.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<HouseScene2_Player>();
    }
    void Update()
    {
        getMemory();
    }

    public void getMemory()
    {
        if (isClickbutton && !hasPlayedBGM)
        {
            gameManager.isLoading = true;
            isEbutton = false;
            Video.SetActive(true);

            player.isTalk1 = true;
            BGM.Stop();
            Memory.Play();

            Invoke("ReStart", 38f);
            isFin = true;
            hasPlayedBGM = true;
        }
    }

    public void OnEButtonClick()
    {
        if (isNear && !isFin)
        {
            isClickbutton = true;
            Ebutton.SetActive(false);
            getMemory();
        }
    }

    public void ReStart()
    {
        if (isFin && !PlayerData.isEnglish)
        {
            Video.SetActive(false);
            maincam.Priority = 2;
            npccam.Priority = -5;

            npc.SetActive(false);
            BGM.Play();
            gameManager.isLoading = false;
            Wall.SetActive(false);
            Memory.Stop();
            TalkUI1.SetActive(true);
            isFin = false;

        }

        if (isFin && PlayerData.isEnglish)
        {
            Video.SetActive(false);
            maincam.Priority = 2;
            npccam.Priority = -5;

            npc.SetActive(false);
            BGM.Play();
            gameManager.isLoading = false;
            Wall.SetActive(false);
            Memory.Stop();
            TalkUI2.SetActive(true);
            isFin = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNear = true;
            npccam.Priority = 100;
            maincam.Priority = 1;
            Ebutton.SetActive(true);
            isEbutton = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNear = false;
            //npccam.Priority = 1;
            //maincam.Priority = 10;
            Ebutton.SetActive(false);
            isEbutton = false;
        }
    }
}
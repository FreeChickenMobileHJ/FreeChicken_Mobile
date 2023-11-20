using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HouseNPC : MonoBehaviour
{
    public GameObject GetMemoryUI;
    public HouseScenePlayer player;

    public bool isEbutton;
    public bool isClickbutton;
    public GameObject Ebutton;

    public bool isNear;
    public static bool isFinish;
    public bool isSet;
   
    public GameObject npc;
    public AudioSource getMemorySound;
    public MemoryCount memCnt;
    public float t;
    public bool isHouse_1;
    public bool isHouse_2;
    public bool isHouse_3;


    void Start()
    {
        Ebutton.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<HouseScenePlayer>();
        memCnt = memCnt.GetComponent<MemoryCount>();
        t = 0;
    }

    IEnumerator CO_GetMemory()
    {

        if (isClickbutton)
        {
            isEbutton = false;
            npc.SetActive(false);
            getMemorySound.Play();
            GetMemoryUI.SetActive(true);
            Ebutton.SetActive(false);
            if (isHouse_1)
            {
                memCnt.MemCntChange(1, 1);
            }
            else if(isHouse_2)
            {
                memCnt.MemCntChange(2, 2);
            }
            else if (isHouse_3)
            {
                memCnt.MemCntChange(3, 3);
            }

            Invoke("ReStart", 2f);
        }
        yield return null;
    }

    public void OnEButtonClick()
    {
        if (isNear)
        {
            isClickbutton = true;
            StartCoroutine(CO_GetMemory());
        }
    }

    void ReStart()
    {
        GetMemoryUI.SetActive(false);
        isFinish = true;
        this.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            isEbutton = true;
            Ebutton.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            isEbutton = false;
            Ebutton.SetActive(false);
        }
    }
}

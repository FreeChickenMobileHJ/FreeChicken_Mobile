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

    public float t;

    void Start()
    {
        Ebutton.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<HouseScenePlayer>();
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
            MemoryCount.memCount++;

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

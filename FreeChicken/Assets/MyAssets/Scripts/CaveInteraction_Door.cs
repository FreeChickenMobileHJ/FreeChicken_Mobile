using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class CaveInteraction_Door : MonoBehaviour
{
    public GameObject OpenDoorText;
    public GameObject donotOpenDoorText;
    bool isOpen;
    public bool isFin;

    CaveScenePlayer player;
    CaveItem_Key key;

    public AudioSource OpenDoorClear;
  
    public GameObject Thx;
    public GameObject daddy;
    public GameObject Target;
    public GameObject Door;
    Animator dadAnim;
    
    public bool isEnd;
    void Start()
    {
        player = GameObject.Find("CaveCharacter").GetComponent<CaveScenePlayer>();
        dadAnim = daddy.GetComponent<Animator>();
        if (key != null)
        {
            key = GameObject.FindGameObjectWithTag("Key").GetComponent<CaveItem_Key>();
        }
        StartCoroutine("CO_OpenDoor");
    }

    IEnumerator CO_OpenDoor()
    {
        
        //OpenDoor();
        while(true)
        {
           
            if (isEnd)
            {
                dadAnim.SetBool("isWalk", true);
                daddy.transform.position = Vector3.MoveTowards(daddy.transform.position, Target.transform.position, Time.deltaTime * 3f);
                
            }
            yield return null;
        }
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !player.hasKey &&!isEnd)
        {
            donotOpenDoorText.SetActive(true);
            Invoke("CloseText", 2f);
            isOpen = true;
        }

        if(other.gameObject.CompareTag("Player") && player.hasKey && !isEnd && !isFin)
        {
            isOpen = true;
            OpenDoorText.SetActive(true);
        }
    }
    void CloseText()
    {
        donotOpenDoorText.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !player.hasKey && isOpen)
        {
            donotOpenDoorText.SetActive(false);
            isOpen = false;
        }

        if(other.gameObject.CompareTag("Player") && player.hasKey && isOpen)
        {
            isOpen = false;
            OpenDoorText.SetActive(false);
        }
    }

    public void OpenDoor()
    {
        if (isOpen && player.hasKey)
        {
            OpenDoorClear.Play();
            Invoke("SetEnd", 3f);
            
            OpenDoorText.SetActive(false);
            --player.keyCount;

            Door.SetActive(false);
            isFin = true;
            Thx.gameObject.SetActive(true);
            Invoke("Last", 6f);
            
        }
        else if(isOpen &&!player.hasKey)
        {
            donotOpenDoorText.SetActive(true);
            Invoke("DestroyOpenDoorText", 1.5f);
        }
    }

    void SetEnd()
    {
        isEnd = true;
    }

    void Last()
    {
        gameObject.SetActive(false);
        daddy.gameObject.SetActive(false);
        Thx.gameObject.SetActive(false);
    }

    void DestroyOpenDoorText()
    {
        OpenDoorText.gameObject.SetActive(false);
    }
}

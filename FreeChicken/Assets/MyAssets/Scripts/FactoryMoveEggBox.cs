using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class FactoryMoveEggBox : MonoBehaviour
{
    public float Speed;
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera moveCam;
    public FactoryPlayer player;
    public bool isChk;
    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    void Start()
    {
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();

        Speed = 0;
        StartCoroutine(CheckStart());
    }
   
    IEnumerator CheckStart()
    {
        while (true)
        {
            if (player.isSetEggFinish && !isChk)
            {

                Check();
            }
            if (player.isSetEggFinish && player.isEgg)
            {
                player.transform.position = player.tmpBox.gameObject.transform.position;
                player.EggPrefab.transform.position = player.tmpBox.gameObject.transform.position;
            }
            yield return null;
        }
    }
   
    void Check()
    {

        Speed = 0.25f;
       
        mainCam.Priority = 1;
        moveCam.Priority = 2;
        isChk = true;

    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slide"))
        {

            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);
        }
        if (other.CompareTag("TurnPointR"))
        {
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * Speed, Space.World);
        }
        if (other.CompareTag("TurnPointL"))
        {
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * Speed, Space.World);
        }
        if (other.CompareTag("TurnPointD"))
        {
            this.gameObject.transform.Translate(Vector3.back * Time.deltaTime * Speed, Space.World);

        }
    }
  
}

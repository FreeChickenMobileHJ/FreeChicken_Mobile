using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.IO;
public class FactoryPlayer_2 : MonoBehaviour
{
    [Header("Settings")]
    public Animator anim;
    public float speed = 2.5f;
    public float hAxis;
    public float vAxis;
    public float jumpPower;
    Vector3 moveVec;
    Rigidbody rigid;
    public GameObject thisRealObj;

    public VariableJoystick joystick;


    [Header("Bool")]
    public bool isJump;
    public bool isSlide;
    public bool isEbutton;
    public bool isDie;
    public bool isStamp;
    public bool isTalk;
    public bool isStopSlide; 
    public bool isContact;
    public bool isLoading;
    public bool isUnActive;
    public bool isSave;
    [Header("Stats")]
    public GameObject StampTMP;
    public GameObject DieCanvas;
    public GameObject DieParticle;
    public FactorySceneChangeZone changeZone;
    public ParticleSystem jumpParticle;
    public GameObject pickUpParticle;

    public GameObject SpawnPos;
    public MemoryCount memCnt;
    public FactoryNPC npc;
    public GameObject slidePs_1;
    public GameObject slidePs_2;
    public GameObject SavePos;

    [Header("UI")]
    public GameObject scene2LastUI;
    
    public GameManager gameManager;
    public GameObject MemCountUI;
    public GameObject savePointImage;

    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera DieCam;
    public CinemachineVirtualCamera pickUpCam;

    [Header("Audio")]
    public AudioSource jumpAudio;
    public AudioSource BGM;
    public AudioSource dieAudio;
    public AudioSource changeConAudio;
    public AudioSource bombAudio;
    public AudioSource saveAudio;

   
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isTalk = false;
        changeZone = GameObject.Find("ChangeConveyorZone").GetComponent<FactorySceneChangeZone>();
        BGM.Play();
        memCnt = memCnt.GetComponent<MemoryCount>();
        memCnt.MemCntChange(2, 3);
        npc = npc.GetComponent<FactoryNPC>();
       
        StartCoroutine(PickUPStart());
    }
    void Update()
    {

        if (!isTalk && !isDie && !isUnActive)
        {
            Move();
            GetInput();
            Turn();
        }    
    }
    IEnumerator PickUPStart()
    {
        while (true)
        {
           
            if (isStamp)
            {
                this.gameObject.transform.position = StampTMP.transform.position;
                anim.SetBool("isWalk", false);

            }
            if (changeZone.isButton)
            {
                anim.SetBool("isWalk", false);
            }

            yield return null;
        }

    }
    void PickUP()
    {

        DieCanvas.SetActive(true);
        isDie = true;
        dieAudio.Play();
        Invoke("ExitCanvas", 1.5f);
    }

   
    public void GetInput()
    {
        hAxis = joystick.Horizontal;
        vAxis = joystick.Vertical;
    }

    void Move()
    {
        if (!(hAxis == 0 && vAxis == 0))
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            transform.position += moveVec * speed * Time.deltaTime * 1f;
            anim.SetBool("isWalk", true);
        }
        else if (hAxis == 0 && vAxis == 0)
        {
            anim.SetBool("isWalk", false);
        }
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec); 
    }
    public void Jump()
    {
        if (!isJump)
        {
            anim.SetTrigger("doJump");
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            jumpAudio.Play();
            jumpParticle.Play();
        }       
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sense") && !isStamp)
        {
            StampTMP = collision.gameObject;
            pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            isSlide = false;
            isStamp = true;
            thisRealObj.gameObject.transform.localScale = new Vector3(2f, 0.5f, 2f);
            pickUpParticle.SetActive(true);
            Invoke("PickUP", 2f);

        }
        if (collision.gameObject.CompareTag("PickUpPoc") && !isStamp)
        {
            StampTMP = collision.gameObject;
            pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            isSlide = false;
            isStamp = true;
            pickUpParticle.SetActive(true);
            Invoke("PickUP", 2f);

        }

        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Slide") || collision.gameObject.CompareTag("EggBox") || collision.gameObject.CompareTag("Item"))
        {

            isJump = false;
        }
        if (collision.gameObject.CompareTag("ObstacleZone2") || collision.gameObject.CompareTag("Obstacle") &&!isDie)
        {
            isDie = true;
            DieParticle.SetActive(true);
            anim.SetTrigger("doDie");
            dieAudio.Play();
            anim.SetBool("isDie", true);
            DieCanvas.SetActive(true);
            mainCam.Priority = 1;
            DieCam.Priority = 2;
            Invoke("ExitCanvas", 2f);
        }
        
    }
   
    public void ExitCanvas()
    {
       
        DeadCount.count++;
        isStamp = false;
      
        DieParticle.SetActive(false);
        DieCanvas.SetActive(false);
        MemCountUI.SetActive(false);
        anim.SetBool("isDie", false);
        
        isSlide = true;
       
        isDie = false;
        thisRealObj.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
        pickUpParticle.SetActive(false);
        if (!isSave)
        {
            this.gameObject.transform.position = SpawnPos.transform.position;
        }
        else if (isSave)
        {
            this.gameObject.transform.position = SavePos.transform.position;
        }
        pickUpCam.Priority = -1;
       
        DieCam.Priority = 1;
        mainCam.Priority = 2;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Rail")
        {
            if (npc.cnt == 3)
            {
                scene2LastUI.gameObject.SetActive(true);
                Invoke("RoadScene", 2f);
            }
            else if(npc.cnt < 3)
            {
                MemCountUI.gameObject.SetActive(true);
                Invoke("ExitCanvas", 1.5f);
            }
        }
        if (other.CompareTag("Item"))
        {
            
            slidePs_1.SetActive(true);
            slidePs_2.SetActive(true);
            bombAudio.Play();
        }
        if (other.CompareTag("SavePoint_1"))
        {
            isSave = true;
            saveAudio.Play();
            savePointImage.SetActive(true);
            other.gameObject.SetActive(false);
            Invoke("SetActiveFalseImage", 2f);
        }
    }
    void SetActiveFalseImage()
    {
        savePointImage.SetActive(false);
    }
    void RoadScene()
    {
        GameSave.Level = 4;
        LoadingSceneManager.LoadScene("Enter2DScene");     
    }
   
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slide"))
        {

            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 3f, Space.World);


          
        }
        if (other.CompareTag("TurnPointR"))
        {

            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * 1f, Space.World);

        }
        if (other.CompareTag("TurnPointL"))
        {

            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * 1f, Space.World);

        }
        if (other.CompareTag("TurnPointD"))
        {

            this.gameObject.transform.Translate(Vector3.back * Time.deltaTime * 1f, Space.World);

        }
        if (other.CompareTag("Item"))
        {
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 15f, Space.World);
           
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slide"))
        {
            isSlide = false;
        }

       
    }


}

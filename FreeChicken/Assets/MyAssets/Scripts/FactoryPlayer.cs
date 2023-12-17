using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.IO;

public class FactoryPlayer : MonoBehaviour
{
    [Header("Setting")]
    public GameObject thisMesh;
    public GameObject thisRealObj;
    public Animator anim;
    public float speed = 2.5f;
    public float hAxis;
    public float vAxis;
    public float jumpPower;
    Vector3 moveVec;
    Rigidbody rigid;
    public ParticleSystem jumpParticle;
    public VariableJoystick joystick;

    [Header("Bool")]
    public bool isFactory_2;
    public bool isJump;
    public bool isSlide;
    public bool isEbutton;
    public bool isDie;
    public bool isStamp;

    public bool isTalk;
    public bool isEgg;
    public bool isStopSlide;

    public bool isContactStopSlide;

    public bool isContact;
    public bool isSetEggFinish;
    public bool isStart;
    public bool isStopCon;
    public bool isPickUp;

    public bool isSavePointPos;

    public bool isPlaying = false;

    public int DeathCount;

    public bool isWallChagneColor;
    public bool isClick;

    public bool isEnglish;
    public bool isChk;
    public bool isUnActive;
    [Header("UI")]
    public GameObject turnEggCanvas;
    public GameObject changeEggCanvas;
    public GameObject stopSlideCanvas;
    public GameObject tmpBox;
    public TextMeshProUGUI E;
    public TextMeshProUGUI Spacebar;
    public GameObject DieCanvas;
    public GameObject UpstairCanvas;
    public GameObject scene2LastUI;
   
    public GameObject SavePointTxt;
    public GameObject loadingUI;
    
    [Header("Stats")]
    public GameObject eggBoxSpawnTriggerBox;
    public GameObject eggBox;
    public GameObject eggBoxSpawnPos;
    public GameObject EggPrefab;

    
    public GameObject DieParticle;
    public GameObject PickUpParticle;
    public Vector3 pos;

    public GameObject TMP;
    public GameObject StampTMP;

    public GameObject RedPos;
    public GameObject YellowPos;
    public GameObject GreenPos;
    public GameObject BluePos;

    public GameObject existingSlideObj;
    public GameObject changeSlideObj;

    public GameObject savePointPos;
    public GameObject savePointPos_1;
    public GameObject savePointPos_2;

    public GameObject ChangeEggDoor;

    public GameObject BlockWall;
    public GameManager gameManager;
    public LoadingTyping Loading;

    public MemoryCount memCnt;
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera stopConCam;
    public CinemachineVirtualCamera managerCam;
    public CinemachineVirtualCamera dieCam;
    public CinemachineVirtualCamera pickUpCam;
    public CinemachineVirtualCamera eggChangeCam;


    [Header("Audio")]
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;
    public AudioSource eggChangeZoneAudio;
    public AudioSource mainAudio;
    public AudioSource secoundmainAudio; 
    public AudioSource heartBeatAudio;
    public AudioSource fixAudio;
    public bool isEng;
    public bool isKorean;
  
    private void Start()
    {
        mainAudio.Play();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isTalk = false;
        memCnt = memCnt.GetComponent<MemoryCount>();
        memCnt.MemCntChange(0, 1);
       
        if (isFactory_2)
        {
            memCnt.MemCntChange(1, 2);
           
        }
        StartCoroutine(PickUPStart());

    }
    private void Update()
    {
        if (!isUnActive &&!isTalk && !isEgg && !isDie && !isStart && !isPickUp && !isStamp)
        {
            Turn();
            Move();
            GetInput();
        }
    }
    IEnumerator PickUPStart()
    {
        while (true)
        {
            if (isPickUp)
            {
                this.gameObject.transform.position = TMP.transform.position;
                anim.SetBool("isWalk", false);
            }
            if (isStamp)
            {
                this.gameObject.transform.position = StampTMP.transform.position;
                anim.SetBool("isWalk", false);

            }
            yield return null;
        }
      
    }
    void PickUP()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            dieAudio.Play();
        }
        DieCanvas.SetActive(true);
        Invoke("ExitCanvas", 2f);
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
           
            runAudio.Play();

        }
        else if (hAxis == 0 && vAxis == 0)
        {
            anim.SetBool("isWalk", false);
            runAudio.Stop();
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
            jumpParticle.Play();
            jumpAudio.Play();
            isJump = true;


        }


    }
  
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sense") &&!isStamp)
        {
            StampTMP = collision.gameObject;
            PickUpParticle.SetActive(true);
            pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            isSlide = false;
            isStamp = true;
            thisRealObj.gameObject.transform.localScale = new Vector3(2f,0.5f,2f);

            Invoke("PickUP", 2f);

        }
        if(collision.gameObject.CompareTag("PickUpPoc") && !isPickUp)
        {
            TMP = collision.gameObject;
            isPickUp = true;
            isSlide = false;
            pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            PickUpParticle.SetActive(true);
            Invoke("PickUP", 2f);

        }
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Slide") || collision.gameObject.CompareTag("EggBox"))
        {

            isJump = false;
        }
       
        if(collision.gameObject.CompareTag("Floor"))
        {
            
                UpstairCanvas.SetActive(true);
                Invoke("UpstairExit", 2f);
            
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isDie && !isPickUp)
            {
                isDie = true;
                anim.SetTrigger("doDie");
                anim.SetBool("isDie", true);

                DieParticle.SetActive(true);
                DieCanvas.SetActive(true);
                dieCam.Priority = 2;
                mainCam.Priority = 1;

                dieAudio.Play();



                Invoke("ExitCanvas", 2f);
                
            }
            
        }
        
    }

    void UpstairExit()
    {
        UpstairCanvas.SetActive(false);
    }
    void ExitCanvas()
    {

        DeadCount.count++;
        
        if (isDie)
        {
            DieCanvas.gameObject.SetActive(false);
            
            
            dieCam.Priority = -3;
            isDie = false;
            DieParticle.SetActive(false);
            anim.SetBool("isDie", false);
        }
        if(isPickUp)
        {   
           
            isPickUp = false;
            pickUpCam.Priority = -5;
            DieCanvas.SetActive(false);
            PickUpParticle.SetActive(false);
        }
        if (isStamp)
        {
        
            isStamp = false;
            thisRealObj.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            pickUpCam.Priority = -5;    
            DieCanvas.SetActive(false);
            PickUpParticle.SetActive(false);
        }
        isPlaying = false;
        mainCam.Priority = 2;
        Pos();

    }
    public void Pos()
    {
        if (isSavePointPos && isFactory_2)
        {
            this.gameObject.transform.position = savePointPos_1.gameObject.transform.position;
            BlockWall.SetActive(false);
        }
        else if(!isSavePointPos && isFactory_2)
        {
            this.gameObject.transform.position = savePointPos.gameObject.transform.position;
        }
        else
        {
            this.gameObject.transform.position = pos;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
       
        if(other.CompareTag("Rock") && !isChk)
        {
            mainAudio.Stop();
            mainCam.Priority = -1;
            eggChangeCam.Priority = 10;
            BlockWall.SetActive(true);
            eggChangeZoneAudio.Play();
            isTalk = true;
            isChk = true;
            anim.SetBool("isWalk", false);
            turnEggCanvas.gameObject.SetActive(true);
        }
       
        if (other.CompareTag("StopSlide")) 
        {

            
            mainCam.Priority = 1;
            stopConCam.Priority = 2;
            stopSlideCanvas.gameObject.SetActive(true);
            eggBoxSpawnTriggerBox.SetActive(true);
            existingSlideObj.SetActive(false);
            changeSlideObj.SetActive(true);
            isWallChagneColor = true;


        }
        if(other.CompareTag("EggBoxPos"))
        {
            
            Vector3 pos = eggBoxSpawnPos.transform.position;
            Quaternion rotate = new Quaternion(-0.0188433286f, -0.706855774f, -0.706855536f, 0.0188433584f);
          
            eggBox.SetActive(true);
            eggBox.transform.position = pos;
            eggBox.transform.rotation = rotate;
            
           
        }
        
        if(other.CompareTag("SavePoint_1"))
        {
            isSavePointPos = true;
            savePointAudio.Play();
            savePointPos_1.SetActive(false);
            SavePointTxt.SetActive(true);
            Invoke("DestroySavePointTxt", 2f);
        }
       
    }
    
  
    void DestroySavePointTxt()
    {
        SavePointTxt.SetActive(false);
    }

    IEnumerator Egg()
    {
        yield return new WaitForSeconds(1.5f);
        if (isEgg)
        {
            eggChangeCam.Priority = -100;
            ChangeEggDoor.SetActive(false);
            isSetEggFinish = true;
            eggChangeZoneAudio.Stop();
            heartBeatAudio.Play();
            isTalk = false;
            turnEggCanvas.SetActive(false);
            
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slide") && !isTalk && !isStopSlide)
        {
            isSlide = true;
        }
        if (other.CompareTag("TurnPointR") && !isStopSlide)
        {
            
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * 1f, Space.World);
        }
        if (other.CompareTag("TurnPointL") && !isStopSlide)
        {
            
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * 1f, Space.World);
        }
        
       
    }
   


    public void RedChk()
    {
        if (!isSetEggFinish)
        {
            Vector3 pos = RedPos.gameObject.transform.position;

            EggFClick(RedPos, pos);
        }
    }
    public void BlueChk()
    {
        if (!isSetEggFinish)
        {
            Vector3 pos = BluePos.gameObject.transform.position;
            EggFClick(BluePos, pos);
        }
    }
    public void GreenChk()
    {
        if (!isSetEggFinish)
        {
            Vector3 pos = GreenPos.gameObject.transform.position;
            EggFClick(GreenPos, pos);
        }
    }
    public void YellowChk()
    {
        if (!isSetEggFinish)
        {
            Vector3 pos = YellowPos.gameObject.transform.position;
            EggFClick(YellowPos, pos);
        }
    }
    void EggFClick(GameObject Color,Vector3 pos)
    {
        tmpBox = Color.gameObject;
        thisMesh.SetActive(false);
        EggPrefab.gameObject.SetActive(true);
        EggPrefab.transform.position = pos;
        turnEggCanvas.gameObject.SetActive(false);
      
        isEgg = true;
        StartCoroutine(Egg());
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slide"))
        {
            isSlide = false;
        }
        if(other.CompareTag("PointZone"))
        {
            turnEggCanvas.gameObject.SetActive(false);
        }
        if(other.CompareTag("StopSlide"))
        {
            
            mainCam.Priority = 2;
            stopConCam.Priority = 1;
            stopSlideCanvas.gameObject.SetActive(false);
            
        }
        
    }
    
}

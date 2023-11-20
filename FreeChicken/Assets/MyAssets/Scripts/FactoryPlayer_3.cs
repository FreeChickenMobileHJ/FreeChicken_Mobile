using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;
using System.IO;
public class FactoryPlayer_3 : MonoBehaviour
{
    [Header("Settings")]
    public Animator anim;
    public float speed = 2.5f;
    public float hAxis;
    public float vAxis;
    public float jumpPower;
    public int AttackCnt;

    Vector3 moveVec;
    Rigidbody rigid;

    public ParticleSystem attackParticle;
    public ParticleSystem jumpParticle;

    public GameObject DieParticle;

    public GameObject PotionTMP;
    public GameObject PotionTMP_2;

    public GameObject NPC;

    public GameObject StartParticle;
    public LoadingTyping Loading;
    public Joystick joystick;

    public int curDeadCnt;
    //public GameState gameState;
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera changeCam;
    public CinemachineVirtualCamera dieCam;
    public CinemachineVirtualCamera potionCam;
    public CinemachineVirtualCamera potionCam_2;

    [Header("Bool")]
    public bool isJump;
    public bool isSlide;
    public bool isEbutton;
    public bool isDie;
    public bool isFin;
    public bool isStopSlide;
    public bool isContact;
    public bool isAttack;
    public bool isTruckGo;
    public bool isTalk;
    public bool isPickUp;

    public bool isSavePointChk;

    public bool isSavePoint_1;

    public bool isSavePoint_2;
    public bool isPotion;

    public bool isTouchText;
    public bool isChk;
    [Header("UI")]
    public GameObject startUI;
    public GameObject mainUI;

    public GameObject DieCanvas;
    public GameObject ExitUI;
   
    public GameObject UpstairUI;

    public GameObject LastUI;
    public GameObject truckPos;
    public GameObject Truck;
    public TextMeshProUGUI EButton;

    public float minValue;
    public float maxValue;

    public GameObject Pos0;
    public GameObject Pos1;

    public GameObject Pos3;

    public GameObject SavePointObj_1;

    public GameObject SavePointObj_3;

    public GameObject SavePosUI;
    public GameManager gameManager;

    public GameObject HintKorean;
    public GameObject HintEnglish;
    [Header("Audio")]
    public AudioSource jumpAudio;
    public AudioSource dieAudio;
    public AudioSource SaveAudio;
    public AudioSource truckLeaveAudio;
    public AudioSource BGM;
    public AudioSource PipeMagicSound;
    public AudioSource StartSound;
    public MemoryCount memCnt;

    void Awake()
    {
        
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        NPC = GameObject.FindWithTag("NPC");
        memCnt = memCnt.GetComponent<MemoryCount>();
    }
    void Start()
    {
       
        memCnt.MemCntChange(3,4);
        Loading = GetComponent<LoadingTyping>();
        BGM.Play();
        isTalk = true;
        StartSound.Play();
        curDeadCnt = DeadCount.count;
        Invoke("ReStart", 3f);
    }
    void ReStart()
    {
        startUI.SetActive(false);
        StartParticle.SetActive(false);
        isTalk = false;
        StartSound.Stop();
        StartCoroutine(TruckGoStart());
    }
    void Update()
    {

        if (!isTalk && !isDie && !isAttack && !isTruckGo && !isPotion)
        {
            Move();
            GetInput();
            Turn();


        }

        
     

    }
    IEnumerator TruckGoStart()
    {
        while (true)
        {
            if (isTruckGo)
            {
                Truck.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 4f);
                this.gameObject.transform.position = truckPos.transform.position;
                LastUI.SetActive(true);
                Invoke("Finish", 2f);

            }
            if(DeadCount.count == curDeadCnt + 10 && !isChk)
            {
                if (PlayerData.isEnglish)
                {
                    HintEnglish.SetActive(true);
                }
                else if(!PlayerData.isEnglish)
                {
                    HintKorean.SetActive(true);
                }
                isChk = true;
                
                StartCoroutine(UnHintText());
            }
            yield return null;
        }
    }
    IEnumerator UnHintText()
    {
        yield return new WaitForSeconds(3f);
        if (HintKorean.activeSelf == true)
        {
            HintKorean.SetActive(false);
        }
        else if(HintEnglish.activeSelf == true)
        {
            HintEnglish.SetActive(false);
        }
    }
    void Finish()
    {
       
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            GameSave.Level = 5;
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            if (loadedData.LevelChk >= GameSave.Level)
            {
                GameSave.Level = loadedData.LevelChk;
            }
            else
            {
                GameSave.Level = 5;
            }
        }
        else
        {
            GameSave.Level = 5;
        }

        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;


        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
        LoadSceneInfo.is2DEnterScene = true;
        PlayerPrefs.SetInt("Scene2D", LoadSceneInfo.is2DEnterScene ? 1 : 0);
        LoadSceneInfo.LevelCnt = 2;
        SceneManager.LoadScene("LoadingScene");
    }
 
    public void TouchText()
    {
        this.gameObject.transform.position = truckPos.transform.position;

        ExitUI.gameObject.SetActive(false);
        changeCam.Priority = 5;
        mainCam.Priority = -5;

        truckLeaveAudio.Play();
        isEbutton = false;

        isTruckGo = true;
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

            isJump = true;
            jumpParticle.Play();
            jumpAudio.Play();
            anim.SetTrigger("doJump");
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        }

      

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {

            ExitUI.gameObject.SetActive(false);
            isEbutton = false;


        }
    }
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Exit") && !isEbutton) 
        {
            
            ExitUI.gameObject.SetActive(true);
            isEbutton = true;
           
           
        }
      
       if(other.gameObject.CompareTag("SavePoint_1"))
        {
            isSavePointChk = true;
            isSavePoint_1 = true;
           
            isSavePoint_2 = false;
            SavePointObj_1.SetActive(false);
            SaveAudio.Play();
            SavePosUI.SetActive(true);
            Invoke("ResetUI", 3f);
        }
   
       if(other.gameObject.CompareTag("SavePoint_2"))
        {
            isSavePointChk = true;
            isSavePoint_1 = false;
            
            isSavePoint_2 = true;
            SaveAudio.Play();
            SavePointObj_3.SetActive(false);
            SavePosUI.SetActive(true);
            Invoke("ResetUI", 3f);
        }
        if (other.gameObject.CompareTag("Poison"))
        {
            isPotion = true;
            mainCam.Priority = -1;
            potionCam.Priority = 100;
            PipeMagicSound.Play();
            
           
            Invoke("ResetCam", 2f);
            
        }
        if(other.gameObject.CompareTag("Item"))
        {
            isPotion = true;
            mainCam.Priority = -1;
            potionCam_2.Priority = 100;
            PipeMagicSound.Play();
            Invoke("ResetCam_1", 2f);
        }
    }
    void ResetUI()
    {
        SavePosUI.SetActive(false);
    }
    void ResetCam()
    {
        this.gameObject.transform.position = PotionTMP.transform.position;
        
        potionCam.Priority = -5;
        mainCam.Priority = 100;
        isPotion = false;
        PipeMagicSound.Stop();
    }
    void ResetCam_1()
    {
        this.gameObject.transform.position = PotionTMP_2.transform.position;

        potionCam_2.Priority = -5;
        mainCam.Priority = 100;
        isPotion = false;
        PipeMagicSound.Stop();
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Slide") || collision.gameObject.CompareTag("EggBox") || collision.gameObject.CompareTag("Props") || collision.gameObject.CompareTag("PickUpPoc") || collision.gameObject.CompareTag("Poison"))
        {

            isJump = false;
        }
        if (collision.gameObject.CompareTag("ObstacleZone3")|| collision.gameObject.CompareTag("Obstacle")&&!isDie)
        {
            
            isDie = true;
            anim.SetTrigger("doDie");
            anim.SetBool("isDie",true);
            mainCam.Priority = 1;
            dieCam.Priority = 2;
            dieAudio.Play();
            changeCam.Priority = -1;
            DieParticle.SetActive(true);
            DieCanvas.SetActive(true);
            if (!isSavePointChk)
            {
                Invoke("ExitCanvas", 1.5f);
            }
            else if (isSavePointChk)
            {
                Invoke("ReSpawnCanvas", 2f);
                
            }

        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            UpstairUI.gameObject.SetActive(true);
            Invoke("UpstairExit", 1f);
        }
        
    }
   
    void UpstairExit()
    {
        UpstairUI.SetActive(false);
    }

    void ExitCanvas()
    {
        DeadCount.count++;
        DieCanvas.gameObject.SetActive(false);
        isDie = false;
       
        DieParticle.SetActive(false);
        mainCam.Priority = 2;
        dieCam.Priority = 1;
        
        anim.SetBool("isDie", false);
        this.gameObject.transform.position = Pos0.transform.position;
    }
    void ReSpawnCanvas()
    {
        DeadCount.count++;
        DieCanvas.gameObject.SetActive(false);
        isDie = false;

        DieParticle.SetActive(false);
        mainCam.Priority = 2;
        dieCam.Priority = 1;
        
        anim.SetBool("isDie", false);
        if (isSavePoint_1)
        {
            this.gameObject.transform.position = Pos1.transform.position;
        }
        
        if (isSavePoint_2)
        {
            this.gameObject.transform.position = Pos3.transform.position;
        }
    }
}

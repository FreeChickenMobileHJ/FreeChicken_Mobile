using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;

public class CaveScenePlayer : MonoBehaviour
{
    
    [Header("Settings")]
    Vector3 moveVec;
    bool wDown;
    bool Dash;
    public bool Dead;
    public bool isMove;

    Rigidbody rigid;

    public ParticleSystem MoveParticle;
    public ParticleSystem PoisonParticle;
    public ParticleSystem DiePs;

    public float speed;
    public float jumpPower = 5f;
    public float hAxis;
    public float vAxis;
    public float rhAxis;
    public float rvAxis;

    public bool isReversed = false;
    private float reverseDuration =5.0f;
    
    Animator anim;
    
    public int keyCount;
    public VariableJoystick joyStick;
    [Header("Bool")]
    public bool isSense;
    public bool isSenseTest;
    public bool isfallingObstacle;
    public bool isfallingBook;
    public bool isMoveUp;
    bool isJump;
    public bool isTalk;
    public bool hasKey;
    public bool Talk_NPC1;
    public bool Talk_NPC2;
    public bool Talk_NPC3;
    public bool Talk_NPC4;
    public bool Talk_NPC5;
    public bool isChk;
    public bool isDadAnimChk;
    public bool isAnimChk;
    public bool isFollow;
    public bool isFinal;
    public bool isTimerChk;
    public bool isLastUI;
    public bool isFire;
    public bool isCave_2;
    public bool isCave_3;
    public bool isCave_4;
    public bool isCave_5;

    public bool isVibrate;
    public bool canHandleCollision = true;
    public bool isSaveChk;
    public bool MomContacting;
    [Header("Camera")]
    
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera FirstCam;
    public CinemachineVirtualCamera TalkToDaddy2Cam;
    public CinemachineVirtualCamera TogetherCam;
    public CinemachineVirtualCamera FDadTalkCam;
    public CinemachineVirtualCamera NPC4Cam;
    public CinemachineVirtualCamera NPC3Cam;
    public CinemachineVirtualCamera MomCam;
    public CinemachineVirtualCamera mainCam2;
    public CinemachineVirtualCamera FinalCam;
    public CinemachineVirtualCamera DadDieCam;

    [Header("UI")]  
    public GameObject image1;
    public GameObject image1_K;
    public GameObject image1_E;

    public GameObject image2;
    public GameObject image2_K;
    public GameObject image2_E;

    public GameObject image3;
    public GameObject image3_K;
    public GameObject image3_E;

    public GameObject image4;
    public GameObject image4_K;
    public GameObject image4_E;

    public GameObject image5;
    public GameObject image5_K;
    public GameObject image5_E;

    
    public GameObject StopPleaseUI;
    public GameObject GetUpgradeBox_text;
   
    public GameObject LastUI;
    public GameObject Last_K;
    public GameObject Last_E;
    public GameObject TimerUI;

    public GameObject DieCanvas;
    [Header("SavePoint")]
    
    public GameObject SavePointImage;
    public GameObject SavePoint0Obj;
    public GameObject SavePoint1Obj;
    public GameObject SavePoint2Obj;
    public GameObject SavePoint3Obj;
    public GameObject SavePoint4Obj;
    public GameObject SavePoint5Obj;
    public bool check_savepoint0;
    public bool check_savepoint1;
    public bool check_savepoint2;
    public bool check_savepoint3;
    public bool check_savepoint4;
    public bool check_savepoint5;

    [Header("Audio")]
    public AudioSource savePointAudio;
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource TalkAudio;
    public AudioSource OpenDoorAudio;
    public AudioSource debuffAudio;
    public AudioSource ElevtorAudio;
    public AudioSource fallingAudio;
    public AudioSource BoxGetAudio;
    public AudioSource countDownSound;

    [Header("Mom")]
    public GameObject Mom;
    public GameObject FollowMom;
    Animator MomDownAnim;
    bool isMomContact;

    [Header("Dad")]
    public GameObject Dad;
    public GameObject DadDieUI;
    public GameObject DadTImerUI;
    public AudioSource BombSound;
    public GameObject BombParticle;
    public GameObject ReStartZone;
    public GameObject DadReStartZone;
    public GameObject KissZone;
    Animator DadAnim;

    Animator NpcDad;
    public GameObject KissParticle;

   
    public GameObject GetUpgradePs;

    public GameManager gameManager;
    public CityMap_CountDown cnt;
    public TextMeshProUGUI vibrateText;

    public Vector3 Pos_0;
    public Vector3 Pos_1;
    public Vector3 Pos_2;
    public Vector3 Pos_3;
    public Vector3 Pos_4;
    public Vector3 Pos_5;
   
    public Button DashButton;
   
    void Awake()
    {
        Application.targetFrameRate = 30;
        anim = GetComponentInChildren<Animator>();
        MomDownAnim = Mom.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        DadAnim = Dad.GetComponent<Animator>();
        mainAudio.Play();
    }

    void Start()
    {
        if (PlayerData.isVibrate)
        {
            isVibrate = true;
            vibrateText.color = Color.white;
        }
        else if (!PlayerData.isVibrate)
        {
            isVibrate = false;
            vibrateText.color = Color.black;
        }
        DiePs.gameObject.SetActive(false);
        cnt = GameObject.FindGameObjectWithTag("TimerCnt").GetComponent<CityMap_CountDown>();
      
        if (isCave_2)
        {
            image1.SetActive(false);
            image2.SetActive(false);
            check_savepoint2 = true;
        }
        if (isCave_3)
        {
            check_savepoint3 = true;
        }
        if (isCave_4)
        {
            check_savepoint4 = true;
        }
        if (isCave_5)
        {
            check_savepoint5 = true;
        }

        //StartCoroutine("CO_notDead");
        StartCoroutine("CO_MomContact");
        StartCoroutine("CO_TalkNPC2");
        StartCoroutine("CO_TimerCheck");
    }
    private void Update()
    {
        if (!Dead && !isReversed && !isTalk)
        {
            Move();

            GetInput();

        }
        if (!Dead && isReversed && !isTalk)
        {
            ReversalMove();

            GetInput();

        }
        if (isTalk)
        {
            anim.SetBool("isRun", false);
        }
       
    }
 
    IEnumerator CO_MomContact()
    {
        while(true)
        {
            if (isMomContact && Mom.gameObject.transform.position.x <= KissZone.transform.position.x && !isAnimChk)
            {
                isAnimChk = true;
                Dad.SetActive(true);
                KissMovement();

            }
            yield return null;
        }
    }

    IEnumerator CO_TalkNPC2()
    {
        while(true)
        {
            if (image2 == null && Talk_NPC2 && !isDadAnimChk)
            {
                isTalk = true;
                isDadAnimChk = true;
                TalkToDaddy2Cam.Priority = 1;
                mainCam.Priority = 10;
                TalkAudio.Pause();
                GameObject Daddy2 = GameObject.Find("Daddy");
                NpcDad = Daddy2.GetComponent<Animator>();
                NpcDad.SetTrigger("DadDown");
                StartCoroutine(NpcDadDestroy(Daddy2));
            }
            yield return null;
        }
    }

    IEnumerator CO_TimerCheck()
    {
        while(true)
        {
            if (isTimerChk && cnt.isFin) 
            {
                DeadCheck();
                isTimerChk = false;
            }
            yield return null;
        }
    }
    public void SetVibrate()
    {
        if (isVibrate)
        {
            isVibrate = false;
            vibrateText.color = Color.black;
            PlayerData.isVibrate = false;
           
        }
        else
        {
            isVibrate = true;
            vibrateText.color = Color.white;
            PlayerData.isVibrate = true;
         
        }
    }

    void KissMovement()
    {
        KissParticle.SetActive(true);
        MomDownAnim.SetTrigger("Kiss");
        DadAnim.SetTrigger("Kiss");
        
    }
    void GetInput()
    {
        if (!isTalk && !Dead)
        {
            hAxis = joyStick.Horizontal;
            vAxis = joyStick.Vertical;
            
        }
    }
    private void FixedUpdate()
    {
        if (moveVec != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVec);
        }
      
    }
    public void DashAct()
    {
        
        if (!Dash && !isTalk)
        {
            
            Dash = true;
            if (isVibrate)
            {
                Handheld.Vibrate();
            }
        }
    }
    public void DashUnAct()
    {
        if (Dash && !isTalk)
        {
            Dash = false;
        }
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

      
       
        rigid.MovePosition(transform.position + moveVec * speed * (Dash ? 2.5f : 1f) * Time.deltaTime);
        runAudio.Play();
        
        transform.LookAt(transform.position + moveVec); 
        anim.SetBool("isDash", Dash);
        anim.SetBool("isRun", moveVec != Vector3.zero);
       
      
        ShowMoveParticle();
        PoisonParticle.gameObject.SetActive(false);
    }

    public void Jump()
    {
        if (!isJump && !Dead && !isTalk)
        {
            jumpAudio.Play();
            isJump = true;
            isMove = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void ReversalMove() 
    {
        rhAxis = joyStick.Vertical;
        rvAxis = joyStick.Horizontal;

        moveVec = new Vector3(rhAxis, 0, rvAxis).normalized;
        rigid.MovePosition(transform.position + moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime);
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        transform.LookAt(transform.position + moveVec); 

        ShowMoveParticle();
        PoisonParticle.gameObject.SetActive(true);
        runAudio.Play();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Basement_Var3")
        {
            ElevtorAudio.Play();
        }


        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
        if (canHandleCollision)
        {
            if (collision.gameObject.CompareTag("Obstacle") && !Dead)
            {
           
                canHandleCollision = false;
                Dead = true;
               
                DeadCount.count += 1;

                DieMotion();
                StartCoroutine(ReactivateCollision());
               
            }
        }
    }
    private IEnumerator ReactivateCollision()
    {
      
        yield return new WaitForSeconds(4f);
        canHandleCollision = true;
    }


    void isFireSetFalse()
    {
        isFire = false;
    }
    void OnParticleCollision(GameObject other)
    {
        if (canHandleCollision)
        {
            if (other.CompareTag("Fire") && !Dead && !isFire )
            {    
                canHandleCollision = false;
                isFire = true;
                Dead = true;
                //DieCanvas.SetActive(false);
                other.gameObject.SetActive(false);
               
                DeadCount.count += 1;
                DieMotion();
                StartCoroutine(ReactivateCollision());
                Invoke("isFireSetFalse", 5f);
            }
        }
    }

  

    void HideGetupgrade_textAfterDelay()
    {
        GetUpgradeBox_text.SetActive(false);
        GetUpgradePs.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "LastTalkPoint" && !isLastUI)
        {
            if(LastUI!=null) LastUI.SetActive(true);
            isLastUI = true;
            if (PlayerData.isEnglish)
            {
                Last_E.SetActive(true);
            }
            else if(!PlayerData.isEnglish)
            {
                Last_K.SetActive(true);
            }
        }
        if (other.gameObject.name == "UpgradeBox")
        {
            BoxGetAudio.Play();
            GetUpgradeBox_text.SetActive(true);
           
            other.gameObject.SetActive(false);
            Invoke("HideGetupgrade_textAfterDelay", 3f);
            
        }
        
        if (other.CompareTag("SenseTest"))
        {
            isSenseTest = true;
        }

        if (other.CompareTag("DropBox"))
        {
            fallingAudio.Play();
            isfallingObstacle = true;
        }

        if(other.gameObject.name == "SpawnObstaclePos2")
        {
            fallingAudio.Play();
            isfallingBook = true;
        }


        if (other.CompareTag("Door") && !isMoveUp)
        {
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 3f, other.gameObject.transform.position.z);
        }

       

        if (other.CompareTag("NPC1") && !Talk_NPC1)
        {
            image1.SetActive(true);
            isTalk = true;
            FDadTalkCam.Priority = 10;
            this.gameObject.transform.LookAt(other.gameObject.transform.position);
            mainCam.Priority = 1;
            TalkAudio.Play();
            image2.SetActive(false);
            image3.SetActive(false);
            image4.SetActive(false);
            image5.SetActive(false);
            if (PlayerData.isEnglish)
            {
                image1_E.SetActive(true);
            }
            else if (!PlayerData.isEnglish)
            {
                image1_K.SetActive(true);
            }
            Talk_NPC1 = true;
        }

        if (other.CompareTag("NPC2") && !Talk_NPC2)
        {
            image2.SetActive(true);
            TalkAudio.Play();
            TalkToDaddy2Cam.Priority = 10;
            mainCam.Priority = 1;
            this.gameObject.transform.LookAt(other.gameObject.transform.position);
            image5.SetActive(false);
            if (PlayerData.isEnglish)
            {
                image2_E.SetActive(true);
            }
            else if (!PlayerData.isEnglish)
            {
                image2_K.SetActive(true);
            }
            Talk_NPC2 = true;
           
        }

        if (other.CompareTag("NPC3")&& !Talk_NPC3)
        {
            image3.SetActive(true);

            NPC3Cam.Priority = 10;
            mainCam.Priority = 1;
            this.gameObject.transform.LookAt(other.gameObject.transform.position);
            TalkAudio.Play();
            if (PlayerData.isEnglish)
            {
                image3_E.SetActive(true);
            }
            else if (!PlayerData.isEnglish)
            {
                image3_K.SetActive(true);
            }
            image2.SetActive(false);
            image4.SetActive(false);
            image5.SetActive(false);
            
            Talk_NPC3 = true;
        }

        if (other.CompareTag("NPC4") && !Talk_NPC4)
        {
            image4.SetActive(true);
            NPC4Cam.Priority = 10;
            mainCam.Priority = 1;
            this.gameObject.transform.LookAt(other.gameObject.transform.position);
            TalkAudio.Play();
           
            image2.SetActive(false);
           
            image5.SetActive(false);
            if (PlayerData.isEnglish)
            {
                image4_E.SetActive(true);
            }
            else if (!PlayerData.isEnglish)
            {
                image4_K.SetActive(true);
            }
            Talk_NPC4 = true;
        }

        if (other.CompareTag("NPC5") && !Talk_NPC5)
        {
            MomContacting = true;
            image5.SetActive(true);
            MomCam.Priority = 10;
            mainCam.Priority = 1;
            
            TalkAudio.Play();
            if (PlayerData.isEnglish)
            {
                image5_E.SetActive(true);
            }
            else if (!PlayerData.isEnglish)
            {
                image5_K.SetActive(true);
            }
            Talk_NPC5 = true;
        }

        if (other.CompareTag("TogetherSense") && !isTimerChk)
        {
            TogetherCam.Priority = 10;
            mainCam.Priority = 0;
            cnt.isStart = true;
            isTimerChk = true;
            TimerUI.SetActive(true);
        }

        if(other.CompareTag("TogetherSenseOut") && !isChk && isTimerChk)
        {
            TogetherCam.Priority = 0;
            mainCam.Priority = 10;
            isChk = true;
            isTimerChk = false;
            TimerUI.SetActive(false);
            countDownSound.Stop();
            Invoke("DeadCheck", 3f);
        }

        if(other.CompareTag("SenseOut"))
        {
            mainCam2.Priority = 0;
            mainCam.Priority = 10;
        }

        if (other.CompareTag("FinalStart")&& !isFinal)
        {
            mainCam.Priority = 0;
            FinalCam.Priority = 100;
            isFinal = true;
            FollowMom.SetActive(true);
            Mom.SetActive(false);

            KissParticle.SetActive(false);
        }

        if (other.gameObject.name == "StartMainCam")
        {
            mainCam.Priority = 10;
            FirstCam.Priority = 0;
        }

        

        if (other.gameObject.name == "SavePoint0")
        {
            check_savepoint0 = true;
            check_savepoint1 = false;
            check_savepoint2 = false;
            check_savepoint3 = false;
            check_savepoint4 = false;
           
        }

        if (other.CompareTag("SavePoint1") && !isSaveChk)
        {
            check_savepoint1 = true;
            check_savepoint0 = false;
            check_savepoint2 = false;
            check_savepoint3 = false;
            check_savepoint4 = false;
            savePointAudio.Play();
            isSaveChk = true;
            StartCoroutine("GetSavePointImage");
            Invoke("Destroy_SavePointObj1", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.CompareTag("SavePoint2") && !isCave_2 && !isSaveChk)
        {
            check_savepoint2 = true;
            check_savepoint0 = false;
            check_savepoint1 = false;
            check_savepoint3 = false;
            check_savepoint4 = false;
            savePointAudio.Play();
            isSaveChk = true;
            StartCoroutine("GetSavePointImage");
            Invoke("Destroy_SavePointObj2", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.CompareTag("SavePoint3") && !isCave_3 && !isSaveChk)
        {
            check_savepoint3 = true;
            check_savepoint0 = false;
            check_savepoint1 = false;
            check_savepoint2 = false;
            check_savepoint4 = false;
            savePointAudio.Play();
            isSaveChk = true;
            StartCoroutine("GetSavePointImage");
            Invoke("Destroy_SavePointObj3", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.CompareTag("SavePoint4") && !isCave_4 && !isSaveChk)
        {
            check_savepoint4 = true;
            check_savepoint0 = false;
            check_savepoint1 = false;
            check_savepoint2 = false;
            check_savepoint3 = false;
            savePointAudio.Play();
            isSaveChk = true;
            StartCoroutine("GetSavePointImage");
            Invoke("Destroy_SavePointObj4", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.CompareTag("SavePoint5") && !isCave_5 && !isSaveChk)
        {
            check_savepoint5 = true;
            check_savepoint0 = false;
            check_savepoint1 = false;
            check_savepoint2 = false;
            check_savepoint3 = false;
            check_savepoint4 = false;
            savePointAudio.Play();
            isSaveChk = true;
            StartCoroutine("GetSavePointImage");
            Invoke("Destroy_SavePointObj5", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.gameObject.name == "FinalPoint")
        {
            LoadSceneInfo.isEndScene = true;
            PlayerPrefs.SetInt("SceneEnd", LoadSceneInfo.isEndScene ? 1 : 0);
            LoadSceneInfo.LevelCnt = 3;
            SceneManager.LoadScene("LoadingScene");
        }

      
        if(other.CompareTag("Poison")&&!isReversed)
        {
            StartCoroutine(ReversePlayerMovement());
            debuffAudio.Play();
        }

        if (other.gameObject.CompareTag("Item") &&!isFollow)
        {
            StopPleaseUI.SetActive(true);
            Invoke("StopPleaseUIEnd", 3f);
        }
    }
  
    private IEnumerator ReversePlayerMovement()
    {
        isReversed = true;
       
        yield return new WaitForSeconds(reverseDuration);
        isReversed = false;
        debuffAudio.Stop();
    }

    void StopPleaseUIEnd()
    {
        StopPleaseUI.SetActive(false);

        isFollow = true;
       

    }
    void OnTriggerExit(Collider other)
    {
        

        if (other.CompareTag("NPC5") && !isMomContact &&!isTalk)
        {
            MomDownAnim.SetTrigger("Down");
            TogetherCam.Priority = 10;
            isMomContact = true;
            
        }
    }
    IEnumerator NpcDadDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(1.7f);
        obj.SetActive(false);
        isTalk = false;
    }
    void DeadCheck()
    {
        if (Dad.activeSelf == true)
        {
            isTalk = true;
            if (cnt.isFin)
            {
                DadTImerUI.SetActive(true);
            }
            else
            {
                cnt.isStop = true;
                countDownSound.Stop();
                DadDieUI.SetActive(true);
            }
           
            TogetherCam.Priority = 0;
            DadDieCam.Priority = 10;
            TimerUI.SetActive(false);
            BombSound.Play();
            dieAudio.Play();
            BombParticle.SetActive(true);
            
            Invoke("ReStartLastZone", 3f);
        }
    }
    void ReStartLastZone()
    {
        if (cnt.isFin)
        {
            DadTImerUI.SetActive(false);
        }
        else
        {
            DadDieUI.SetActive(false);
        }
        isTalk = false;
        DadDieCam.Priority = 0;
        mainCam.Priority = 10;
        isChk = false;
        cnt.isFin = false;
        BombParticle.SetActive(false);
        cnt.CountValue = 15;
        isTimerChk = false;
        this.gameObject.transform.position = ReStartZone.transform.position;
        Dad.gameObject.transform.position = DadReStartZone.transform.position;
    }
    void ShowMoveParticle()
    {
        if (moveVec != Vector3.zero)
        {
            MoveParticle.gameObject.SetActive(true);
        }
        else if (moveVec == Vector3.zero)
        {
            MoveParticle.gameObject.SetActive(false);
        }
    }

    IEnumerator GetSavePointImage()
    {
        SavePointImage.gameObject.SetActive(true);
        savePointAudio.Play();
        yield break;
    }

    void Destroy_SavePointImage()
    {
        SavePointImage.gameObject.SetActive(false);
        isSaveChk = false;
    }

    //------------Destroy_SavePointObj-----------------------------------------
    void Destroy_SavePointObj1()
    {
        SavePoint1Obj.gameObject.SetActive(false);
      
    }

    void Destroy_SavePointObj2()
    {
        SavePoint2Obj.gameObject.SetActive(false);
        GameSave.Level = 12;

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
            {
                
                string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
                PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

                if (loadedData.LevelChk >= GameSave.Level)
                {
                    GameSave.Level = loadedData.LevelChk;
                }
            else
            {
                GameSave.Level = 12;
            }
        }
        else
        {
            GameSave.Level = 12;
        }

        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;


        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);

        LoadSceneInfo.LevelCnt = 2;

        SceneManager.LoadScene("LoadingScene");

    }

    void Destroy_SavePointObj3()
    {
        SavePoint3Obj.gameObject.SetActive(false);
        GameSave.Level = 13;

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {

            string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            if (loadedData.LevelChk >= GameSave.Level)
            {
                GameSave.Level = loadedData.LevelChk;
            }
            else
            {
                GameSave.Level = 13;
            }
        }
        else
        {
            GameSave.Level = 13;
        }

        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;


        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);

        LoadSceneInfo.LevelCnt = 2;

        SceneManager.LoadScene("LoadingScene");

    }

    void Destroy_SavePointObj4()
    {
        SavePoint4Obj.gameObject.SetActive(false);
        GameSave.Level = 14;

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {

            string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            if (loadedData.LevelChk >= GameSave.Level)
            {
                GameSave.Level = loadedData.LevelChk;
            }
            else
            {
                GameSave.Level = 14;
            }
        }
        else
        {
            GameSave.Level = 14;
        }

        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;


        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);

        LoadSceneInfo.LevelCnt = 2;

        SceneManager.LoadScene("LoadingScene");

    }
    void Destroy_SavePointObj5()
    {
        //SavePoint5Obj.gameObject.SetActive(false);
        GameSave.Level = 15;

        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {

            string jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            if (loadedData.LevelChk >= GameSave.Level)
            {
                GameSave.Level = loadedData.LevelChk;
            }
            else
            {
                GameSave.Level = 15;
            }
        }
        else
        {
            GameSave.Level = 15;
        }

        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;


        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);

        LoadSceneInfo.LevelCnt = 2;

        SceneManager.LoadScene("LoadingScene");

    }
    //------------restart_stage-----------------------------------------
    void restart_stage0()
    {
       
        rigid.MovePosition(Pos_0);
         FirstCam.Priority = 999;      
    }
    void restart_stage1()
    {       
        rigid.MovePosition(Pos_1);
        //this.transform.position = SavePoint1Obj.gameObject.transform.position;      
    }
    void restart_stage2()
    {
        rigid.MovePosition(Pos_2);
        // this.transform.position = SavePoint2Obj.gameObject.transform.position;
    }
    void restart_stage3()
    {
        rigid.MovePosition(Pos_3);
        //this.transform.position = SavePoint3Obj.gameObject.transform.position;        
    }
    void restart_stage4()
    {       
        rigid.MovePosition(Pos_4);
        //this.transform.position = SavePoint4Obj.gameObject.transform.position;      
    }
    void restart_stage5()
    {
        rigid.MovePosition(Pos_5);    
        //this.transform.position = SavePoint5Obj.gameObject.transform.position;       
    }
    void remove_dieUI()
    {
        DiePs.gameObject.SetActive(false);
        anim.SetBool("isDead",false);
        Dead = false;
       
        if (check_savepoint0)
        {
            restart_stage0();          
        }
        else if (check_savepoint1)
        {
            restart_stage1();
        }
        else if (check_savepoint2)
        {
            restart_stage2();
        }
        else if (check_savepoint3)
        {
            restart_stage3();          
        }
        else if (check_savepoint4)
        {
            restart_stage4();          
        }
        else if (check_savepoint5)
        {
            restart_stage5();
        }

        DashButton.interactable = true;
        if (PlayerData.isVibrate)
        {
            isVibrate = true;
            vibrateText.color = Color.white;
        }
        else if (!PlayerData.isVibrate)
        {
            isVibrate = false;
            vibrateText.color = Color.black;
        }
     
    }

    void DieMotion()
    {
        if (Dead )
        {

           
             hAxis = 0;
             vAxis = 0;
             Dash = false;
             isVibrate = false;
 
         
            Input.ResetInputAxes();
           
            DashButton.interactable = false;
           
            dieAudio.Play();          
            DiePs.gameObject.SetActive(true);
            anim.SetTrigger("doDead");           
            anim.SetBool("isDead", true);
            Invoke("remove_dieUI", 3f);
        }
    }
}


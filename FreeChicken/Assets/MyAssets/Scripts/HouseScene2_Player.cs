using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Cinemachine;
using UnityEngine.SocialPlatforms;
using System.IO;
//using UnityEngine.UIElements;

public class HouseScene2_Player : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] public Transform cameraArm;

    public GameObject player;
    public bool isfallingObstacle;

    public GameObject DieCanvas;

    bool isJump;
    Rigidbody rigid;

    public float speed;
    public float JumpPower;

    bool Dead;
    public bool isMove;

    public ParticleSystem DiePs;
    public ParticleSystem JumpPs;

    Animator anim;

    [Header("UI")]
    public GameObject moveJoyStick;
    public GameObject jumpJoyStick;
    public GameObject cameraJoyStick;

    [Header("Camera")]
    public CinemachineVirtualCamera npc_cam;
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera unicycleCam;

    [Header("Audio")]
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;
    public AudioSource trumpetAudio;

    [Header("Dialogue")]
    public GameObject NPCDialogue1;
    public GameObject NPCDialogue2;
    public GameObject NPC;
    public GameObject UnicycleDialogue1;
    public GameObject UnicycleDialogue2;
    public bool isTalk1;
    public bool TalkEnd1;
    public bool isTalk2;
    public bool TalkEnd2;

    public GameObject Pos;
    public GameObject Pos2;

    public GameObject EvolutionPlayer;
    public GameObject EvolutionSense;

    private bool isRotating = false;
    private Quaternion originalCameraRotation;
    private float rotationTimer = 0.0f;
    private float rotationDuration = 3.0f;
    public GameObject EvoluPs;
    public GameObject obj;

    void Awake()
    {
        mainAudio.Play();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    void Start()
    {
        DiePs.gameObject.SetActive(false);
        DieCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if(this.gameObject.transform.position.y <= -100f && !Dead)
        {
            Dead = true;
            DieMotion();
            Invoke("ReLoadScene", 2f);
        }

        if (!Dead)
        {
            if (!isTalk1 || !isTalk2)
            {
                if (isRotating)
                {
                    HandleCameraRotation();
                }
            }
        }
    }

    public void Move(Vector2 moveInput)
    {
        isMove = moveInput.magnitude != 0;
        anim.SetBool("Run", isMove);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveVec = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveVec;
            transform.position += moveVec * speed * Time.deltaTime;
            runAudio.Play();
        }
    }

    public void Jump()
    {
        if (!isJump && !Dead)
        {
            jumpAudio.Play();
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            JumpPs.Play();
        }
    }

    void DieMotion()
    {
        DiePs.gameObject.SetActive(true);
        anim.SetBool("isDead", true);
        dieAudio.Play();
    }

    void ReLoadScene()
    {
        Dead = false;
        anim.SetBool("isDead", false);
        DeadCount.count++;

        if (TalkEnd1)
        {
            this.gameObject.transform.position = Pos2.gameObject.transform.position;
        }
        else if (!TalkEnd1)
        {
            this.gameObject.transform.position = Pos.gameObject.transform.position;
        }
        DiePs.gameObject.SetActive(false);
        DieCanvas.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropBox"))
        {
            isfallingObstacle = true;
        }

        if (other.gameObject.CompareTag("NPC") && !isTalk1 && !TalkEnd1 && !PlayerData.isEnglish)
        {
            isTalk1 = true;
            NPCDialogue1.SetActive(true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            obj.SetActive(false);

            npc_cam.Priority = 10;
            mainCam.Priority = 1;
        }

        if (other.gameObject.CompareTag("NPC") && !isTalk1 && !TalkEnd1 && PlayerData.isEnglish)
        {
            isTalk1 = true;
            NPCDialogue2.SetActive(true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            obj.SetActive(false);

            npc_cam.Priority = 10;
            mainCam.Priority = 1;
        }

        if (other.gameObject.name == "Unicycle_Sense" && !isTalk2 && !TalkEnd2 && !PlayerData.isEnglish)
        {
            isTalk2 = true;
            UnicycleDialogue1.SetActive(true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            unicycleCam.Priority = 10;
            mainCam.Priority = 1;
        }

        if (other.gameObject.name == "Unicycle_Sense" && !isTalk2 && !TalkEnd2 && PlayerData.isEnglish)
        {
            isTalk2 = true;
            UnicycleDialogue2.SetActive(true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            unicycleCam.Priority = 10;
            mainCam.Priority = 1;
        }

        if (other.gameObject.name == "EvolutionSense1")
        {
            trumpetAudio.Play();
            StartRotation();
            Invoke("Destroy_", 2f);
        }

        if (other.gameObject.name == "NextScenePos")
        {
            Invoke("NextScene", 1.5f);
        }
    }

    void NextScene()
    {
        /*LoadSceneInfo.isHouse_2 = true;
        PlayerPrefs.SetInt("SceneHouse_2", LoadSceneInfo.isHouse_2 ? 1 : 0);*/

        if (File.Exists("PlayerData.json"))
        {
            GameSave.Level = 9;
            string jsonData = File.ReadAllText("playerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            if (loadedData.LevelChk >= GameSave.Level)
            {
                GameSave.Level = loadedData.LevelChk;
            }
            else
            {
                GameSave.Level = 9;
            }
        }
        else
        {
            GameSave.Level = 9;
        }


        LoadSceneInfo.LevelCnt = 2;

        SceneManager.LoadScene("LoadingScene");
    }

    void Destroy_()
    {
        Destroy(this.gameObject);
        EvolutionPlayer.SetActive(true);
        EvolutionSense.SetActive(false);
        moveJoyStick.SetActive(false);
        jumpJoyStick.SetActive(false);
        cameraJoyStick.SetActive(false);
    }

    private void HandleCameraRotation()
    {
        rotationTimer += Time.deltaTime;

        float rotationAngle = Mathf.Lerp(0f, 720f, rotationTimer / rotationDuration); 

        cameraArm.RotateAround(transform.position, Vector3.up, rotationAngle * Time.deltaTime);
        EvoluPs.SetActive(true);

        if (rotationTimer >= rotationDuration)
        {
            rotationTimer = 0.0f;
            isRotating = false;

            cameraArm.rotation = originalCameraRotation;
            EvoluPs.SetActive(false);
        }
    }

    public void StartRotation()
    {
        isRotating = true;
        originalCameraRotation = cameraArm.rotation; 
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            npc_cam.Priority = 1;
            mainCam.Priority = 10;

            TalkEnd1 = true;
        }

        if (other.gameObject.name == "Unicycle_Sense")
        {
            unicycleCam.Priority = 1;
            mainCam.Priority = 10;

            TalkEnd2 = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("House2_Obstacle") && !Dead)
        {
            Dead = true;
            DieMotion();
            DieCanvas.gameObject.SetActive(true);
            Invoke("ReLoadScene", 2f);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    public void LookAround(Vector3 inputDirection)
    {
        anim.SetBool("Run", false);
        Vector2 mouseDelta = inputDirection;

        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
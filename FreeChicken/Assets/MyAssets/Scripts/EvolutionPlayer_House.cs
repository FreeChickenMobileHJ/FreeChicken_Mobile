using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Cinemachine;
using System.IO;
using UnityEngine.SocialPlatforms;

public class EvloutionPlayer : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] public Transform cameraArm;
    public GameObject player;

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

    public GameObject Pos;

    [Header("UI")]
    public GameObject moveJoyStick;
    public GameObject jumpJoyStick;
    public GameObject cameraJoyStick;

    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera unicycleCam;

    [Header("Audio")]
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;

    [Header("Dialogue")]
    public GameObject ReadygoCity;
    public bool isTalk2;
    public bool TalkEnd2;

    // 진화효과
    private bool isRotating = false;
    private Quaternion originalCameraRotation;
    private float rotationTimer = 0.0f;
    private float rotationDuration = 2.0f;
    public GameObject EvoluPs;



    void Awake()
    {
        //mainAudio.Play();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    void Start()
    {
        moveJoyStick.SetActive(true);
        jumpJoyStick.SetActive(true);
        cameraJoyStick.SetActive(true);
        DiePs.gameObject.SetActive(false);
        DieCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Dead)
        {
            if (!isTalk2)
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
        Dead = true;
        DiePs.gameObject.SetActive(true);
        anim.SetBool("isDead", true);
        dieAudio.Play();
    }

    void ReLoadScene()
    {
        Dead = false;
        anim.SetBool("isDead",false);
        DiePs.gameObject.SetActive(false);
        this.gameObject.transform.position = Pos.gameObject.transform.position;
        //SceneManager.LoadScene("HouseScene2");
        DieCanvas.gameObject.SetActive(false);
    }

    void NextCityScene()
    {
        
    }
  
    void OnTriggerEnter(Collider other)
    {
      

        if (other.CompareTag("evolu")) 
        {
            StartRotation();
            ReadygoCity.SetActive(true);
        }

        //if (other.gameObject.name == "GoCitySense")
        //{
        //    isTalk2 = true;
        //    GameSave.Level = 3;
        //    GameSave.isCity = true;
        //    PlayerData playerData = new PlayerData();
        //    playerData.LevelChk = GameSave.Level;
        //    string json = JsonUtility.ToJson(playerData);

        //    File.WriteAllText("playerData.json", json);

        //    PlayerPrefs.SetInt("GoCity", GameSave.isCity ? 1 : 0);



        //    LoadSceneInfo.is2DEnterScene = true;
        //    PlayerPrefs.SetInt("Scene2D", LoadSceneInfo.is2DEnterScene ? 1 : 0);
        //    LoadSceneInfo.LevelCnt = 2;
        //    SceneManager.LoadScene("LoadingScene");
        //}

        if (other.gameObject.name == "GoCitySense")
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
            GameSave.Level = 10;
            string jsonData = File.ReadAllText("playerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            if (loadedData.LevelChk >= GameSave.Level)
            {
                GameSave.Level = loadedData.LevelChk;
            }
            else
            {
                GameSave.Level = 10;
            }
        }
        else
        {
            GameSave.Level = 10;
        }


        LoadSceneInfo.LevelCnt = 2;

        SceneManager.LoadScene("LoadingScene");
    }

    private void HandleCameraRotation()
    {
        rotationTimer += Time.deltaTime;

        // 회전 각도 계산 (0에서 720도까지)
        float rotationAngle = Mathf.Lerp(0f, 720f, rotationTimer / rotationDuration); // 0부터 720도까지 두 바퀴 회전

        // 회전
        cameraArm.RotateAround(transform.position, Vector3.up, rotationAngle * Time.deltaTime);

        EvoluPs.SetActive(true);

        if (rotationTimer >= rotationDuration)
        {
            rotationTimer = 0.0f;
            isRotating = false;

            // 회전이 완료된 후에 원래 상태로 돌아가는 처리 추가
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
       

        if (other.CompareTag("evolu"))
        {
            other.gameObject.SetActive(false);
            ReadygoCity.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("House2_Obstacle") && !Dead)
        {
            Dead = true;
            DieMotion();
            DieCanvas.gameObject.SetActive(true);
            Invoke("ReLoadScene", 3.5f);
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


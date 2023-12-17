using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Cinemachine;
using System.IO;
using UnityEngine.EventSystems;
public class HouseScenePlayer : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform cameraArm;
    public GameObject player;
    public ParticleSystem DiePs;
    public ParticleSystem JumpPs;
    Animator anim;
    Rigidbody rigid;
    public GameManager gameManager;
    public float speed;
    public float jumpPower = 5f;

    [Header("Bool")]
    public bool isMove;
    public bool isJump;
    public bool Dead;
    public bool isfallingObstacle;
    public bool isSense;
    public bool isDoorOpen =false;
    public bool isReadyDoorOpen = false;
    public bool pushBell = false;
    public bool isOpeningDoor = false;
    public bool isRaisingDoor = false;
    private float doorOpenTimer = 0.0f;
    private float doorOpenDuration = 3.0f;
    private float doorRaiseSpeed = 0.8f;
    private bool shouldLookAround;
    public bool isHouse1;
    public bool isHouse2;
    public bool isEnglish;
    public bool isUnActive;
    public bool isRotating = false;
    private Quaternion originalCameraRotation;
    private float rotationTimer = 0.0f;
    private float rotationDuration = 3.0f;

    [Header ("GameObject")]
    public GameObject startDoor;
    public GameObject DieCanvas;
    public GameObject NextSceneImage;
    public GameObject GetUpgradePs;

    [Header("Dialogue")]
    public GameObject startCanvas1;
    public GameObject startCanvas2;
    public bool isTalk;
    public bool TalkEnd;

    [Header("SavePoint")]
    public GameObject SavePointImage;
    public GameObject SavePoint1Obj;
    public GameObject SavePoint2Obj;
    public GameObject SavePoint3Obj;
    public bool check_savepoint1;
    public bool check_savepoint2;
    public bool check_savepoint3;

    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera StartCam;
    public CinemachineVirtualCamera openDoorCam;
    public GameObject CameraJoy;
    public GameObject Camera;

    [Header("Audio")]
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;
    public AudioSource bellAudio;
    public AudioSource TalkAudio;
    public AudioSource OpenDoorAudio;
    public AudioSource BoxGetAudio;

    [Header("UI")]
    public GameObject PushBell_text;
    public GameObject GetUpgradeBox_text;
    public MemoryCount memCnt;

    void Awake()
    {
        mainAudio.Play();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        if (File.Exists("playerData.json"))
        {
            string jsonData = File.ReadAllText("playerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

            isEnglish = loadedData.isEng;
        }
        memCnt = memCnt.GetComponent<MemoryCount>();
     
    }

    void Start()
    {
        DiePs.gameObject.SetActive(false);
        
        if (isHouse1)
        {
            StartCam.Priority = 10;
            CameraJoy.SetActive(false);
            shouldLookAround= false;
            StartCoroutine(HideGetupgrade_textAfterDelay(3f));
            StartCoroutine(CO_isOpeningDoor());
            StartCoroutine(CO_isRaisingDoor());
        }
        else if(isHouse2)
        {
            shouldLookAround = true;
            check_savepoint2 = true;
        }
        else
        {
            shouldLookAround = true;
            check_savepoint3 = true;
        }
        if (isHouse1)
        {
            memCnt.MemCntChange(0, 1);
        }
        else if (isHouse2)
        {
            memCnt.MemCntChange(1, 2);
        }
        else
        {
            memCnt.MemCntChange(2, 3);
        }
        StartCoroutine(CO_notDead());
    }

    IEnumerator CO_notDead()
    {
        while (isUnActive && !Dead)
        {
            DiePs.gameObject.SetActive(false);

            if (!isTalk)
            {
                if (isRotating)
                {
                    HandleCameraRotation();
                }
            }
            yield return null;
        }
    }

    IEnumerator CO_isOpeningDoor()
    {
        if (isOpeningDoor) yield break; // 이미 실행 중이면 중지

        yield return new WaitUntil(() => isOpeningDoor);

        doorOpenTimer = 0.0f;

        while (doorOpenTimer < doorOpenDuration)
        {
            doorOpenTimer += Time.deltaTime;
            yield return null;
        }

        isOpeningDoor = false;
        isRaisingDoor = true;
       
    }

    IEnumerator CO_isRaisingDoor()
    {
        if (isRaisingDoor) yield break; // 이미 실행 중이면 중지

        yield return new WaitUntil(() => isRaisingDoor);

        while (startDoor.transform.position.y < 2f)
        {
            startDoor.transform.Translate(Vector3.up * doorRaiseSpeed * Time.deltaTime);
            yield return null;
        }

        startDoor.SetActive(false);
        isRaisingDoor = false;
        isTalk = false;
        mainCam.Priority = 10;
        openDoorCam.Priority = 0;
        pushBell = false;
        PushBell_text.SetActive(false);
        isReadyDoorOpen = false;
        isDoorOpen = true;
        Camera.SetActive(false);
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
        if (!isJump && !Dead && !isOpeningDoor)
        {
            jumpAudio.Play();
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            JumpPs.Play(); 
        }
    }

    public void OpenDoor_Final()
    {
        if (!isOpeningDoor && isReadyDoorOpen)
        {
            isOpeningDoor = true;
            pushBell = true;
            bellAudio.Play();
            PushBell_text.SetActive(false);
            StartCam.Priority = 0;
            openDoorCam.Priority = 10;
            isTalk = true;
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            isDoorOpen = true;
        }
    }

    void DieMotion()
    {
        if (DiePs != null) 
        {
            DieCanvas.gameObject.SetActive(true);
            DiePs.gameObject.SetActive(true);
            dieAudio.Play();
            anim.SetTrigger("isDead");
            Invoke("remove_dieUI", 3f);
        }
    }

    private void HandleCameraRotation()
    {
        if (shouldLookAround && isHouse1)
        {
            rotationTimer += Time.deltaTime;

            float rotationAngle = Mathf.Lerp(0f, 720f, rotationTimer / rotationDuration);
            cameraArm.RotateAround(transform.position, Vector3.up, rotationAngle * Time.deltaTime);

            GetUpgradePs.SetActive(true);

            if (rotationTimer >= rotationDuration)
            {
                rotationTimer = 0.0f;
                isRotating = false;

                cameraArm.rotation = originalCameraRotation;
                GetUpgradePs.SetActive(false);
            }
        }
    }

    public void StartRotation()
    {
        isRotating = true;
        originalCameraRotation = cameraArm.rotation;  
    }

    IEnumerator HideGetupgrade_textAfterDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);

            GetUpgradeBox_text.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropBox"))
        {
            isfallingObstacle = true;
        }

        if(other.CompareTag("Sense") && !isTalk && !TalkEnd && !PlayerData.isEnglish)
        {
            startCanvas1.SetActive(true);
            anim.SetBool("Run", false);
            TalkAudio.Play();
        }

        if (other.CompareTag("Sense") && !isTalk && !TalkEnd && PlayerData.isEnglish)
        {
            startCanvas2.SetActive(true);
            anim.SetBool("Run", false);
            TalkAudio.Play();
        }

        if (other.CompareTag("PushButton") && !pushBell)
        {
            PushBell_text.SetActive(true);
            isReadyDoorOpen = true;
        }

        if(other.CompareTag("PushButton") && isDoorOpen)
        {
            PushBell_text.SetActive(false);
        }

        if(other.gameObject.name == "UpgradeBox")
        {
            shouldLookAround= true;
            BoxGetAudio.Play();
            GetUpgradeBox_text.SetActive(true);
            other.gameObject.SetActive(false);
            StartRotation();
            CameraJoy.SetActive(true);
        }

        if (other.gameObject.CompareTag("SavePoint1"))
        {
            check_savepoint1 = true;
            check_savepoint2 = false;
            check_savepoint3 = false;
            SavePointImage.gameObject.SetActive(true);
            savePointAudio.Play();
            Invoke("Destroy_SavePointObj1", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.gameObject.CompareTag("SavePoint2")) 
        {
            check_savepoint2 = true;
            check_savepoint1 = false;
            check_savepoint3 = false;
            SavePointImage.gameObject.SetActive(true);
            savePointAudio.Play();
            Invoke("Destroy_SavePointImage", 2f);
            
            StartCam.Priority = 0;
            mainCam.Priority = 10;
            shouldLookAround = true;
            Invoke("Destroy_SavePointObj2", 1f);
        }

        if (other.CompareTag("SavePoint3"))
        {
            check_savepoint3 = true;
            check_savepoint1 = false;
            check_savepoint2 = false;
            SavePointImage.gameObject.SetActive(true);
            savePointAudio.Play();
            Invoke("Destroy_SavePointImage", 2f);
            Invoke("Destroy_SavePointObj3", 1.5f);
        }

        if (other.gameObject.name == "NextScenePoint")
        {
            Invoke("NextScene", 1.5f);
            
        }

        if (other.CompareTag("Obstacle") && !Dead)
        {
            Dead = true;

            Invoke("RestartStage", 0f);
        }
    }
    void NextScene()
    {      
        GameSave.Level = 8;      
        LoadingSceneManager.LoadScene("Enter2DScene");
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Sense"))
        {
            TalkEnd = true;
            TalkAudio.Pause();
        }

        if (other.CompareTag("PushButton"))
        {
            PushBell_text.SetActive(false);
        }
    }

    void Destroy_SavePointImage()
    {
        SavePointImage.gameObject.SetActive(false);
    }

    void Destroy_SavePointObj1()
    {
        SavePoint1Obj.gameObject.SetActive(false);
    }

    void Destroy_SavePointObj2()
    {
        GameSave.Level = 6;      
        LoadingSceneManager.LoadScene("Enter2DScene");
    }

    void Destroy_SavePointObj3()
    {
        GameSave.Level = 7;      
        LoadingSceneManager.LoadScene("Enter2DScene");
    }

    void restart_stage1()
    {
        Dead = false;
        this.transform.position = SavePoint1Obj.transform.position;
    }

    void restart_stage2()
    {
        Dead = false;
        this.transform.position = SavePoint2Obj.transform.position;
    }

    void restart_stage3()
    {
        Dead = false;
        this.transform.position = SavePoint3Obj.transform.position;
    }

    void remove_dieUI()
    {
        DeadCount.count++;
        DieCanvas.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Dead)
            return;

        if (collision.gameObject.CompareTag("Obstacle") && !Dead)
        {
            Dead = true;
            RestartStage(); // 바로 죽는모션 나오게 수정
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    void RestartStage()
    {
        DieMotion();

        if (check_savepoint1)
            Invoke("restart_stage1", 3f);
        else if (check_savepoint2)
            Invoke("restart_stage2", 3f);
        else if (check_savepoint3)
            Invoke("restart_stage3", 3f);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire") && !Dead)
        {
            Dead = true;

            Invoke("RestartStage", 0f);
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

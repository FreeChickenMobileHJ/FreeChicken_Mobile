using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;
using Unity.VisualScripting;

public class HouseSceneTalkManager2 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject nextText;
    public bool isClickbutton;

    public Queue<string> sentences;
    private string currentSentences;
    public bool isTyping;

    public static HouseSceneTalkManager2 instance;
    public GameObject NpcImage;
    public GameObject PlayerImage;
    public bool isNPCImage;
    public bool isPlayerImage;
    public bool isTalkEnd;

    HouseScene2_Player player;

    public AudioSource TalkSound;
    public AudioSource ClickButtonSound;

    public CinemachineVirtualCamera maincam;
    public CinemachineVirtualCamera npccam;
    public CinemachineVirtualCamera Unicyclecam;
    public CinemachineVirtualCamera StartVideoCam;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
        isTalkEnd = false;
        isPlayerImage = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScene2_Player>();
        player.isTalk1 = true;
        player.isTalk2 = true;

        NpcImage.SetActive(false);
        PlayerImage.SetActive(false);
    }

    public void OndiaLog(string[] lines)
    {
        sentences.Clear();

        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
    }

    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentences = sentences.Dequeue();
            isTyping = true;
            nextText.SetActive(false);
            TalkSound.Play();
            StartCoroutine(Typing(currentSentences));
        }

        if (sentences.Count == 0)
        {
            maincam.Priority = 2;
            npccam.Priority = -5;
            StartVideoCam.Priority = -5;

            Unicyclecam.Priority = -5;

            if (gameObject != null)
            {
                Destroy(gameObject);

            }
            player.isTalk1 = false;
            player.isTalk2 = false;
        }
    }

    void ChangeImage()
    {
        if (isNPCImage)
        {
            isNPCImage = false;
            NpcImage.gameObject.SetActive(false);
            PlayerImage.gameObject.SetActive(true);
            isPlayerImage = true;
        }
        else if (isPlayerImage)
        {
            isNPCImage = true;
            NpcImage.gameObject.SetActive(true);
            PlayerImage.gameObject.SetActive(false);
            isPlayerImage = false;
        }
    }

    IEnumerator Typing(string line)
    {
        text.text = "";
        foreach (char ch in line.ToCharArray())
        {
            text.text += ch;
            
        }
        yield return null;
    }

    public void ClickNextButton()
    {
        if (!isTyping)
        {
            NextSentence();
            ClickButtonSound.Play();
            ChangeImage();
            StartCoroutine(CheckSentences());
        }
    }

    IEnumerator CheckSentences()
    {
        while (!text.text.Equals(currentSentences))
        {
            yield return null;
        }
        nextText.SetActive(true);
        isTyping = false;
    }
}

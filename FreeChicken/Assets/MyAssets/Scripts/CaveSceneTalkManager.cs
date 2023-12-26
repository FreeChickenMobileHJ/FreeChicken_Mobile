using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;

public class CaveSceneTalkManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject nextText;

    public Queue<string> sentences;
    private string currentSentences;
    public bool isTyping;

    public static CaveSceneTalkManager instance;
    public GameObject NpcImage;
    public GameObject PlayerImage;
    public bool isNPCImage;
    public bool isPlayerImage;
    CaveScenePlayer Player;
    public bool isTalkEnd;
    public AudioSource TalkSound;
    public AudioSource ClickButtonSound;

    public CinemachineVirtualCamera NpcCam;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
        isTalkEnd = false;
        isPlayerImage = true;
        Player = GameObject.Find("CaveCharacter").GetComponent<CaveScenePlayer>();
        Player.isTalk = true;
        Cursor.visible = true;

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
            Destroy(instance.gameObject);
            if(NpcCam != null) { NpcCam.Priority = -100; }
            
            Player.isTalk = false;
            Cursor.visible = false;
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

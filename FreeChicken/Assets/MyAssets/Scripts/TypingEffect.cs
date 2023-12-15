using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI text;
   
    public GameObject TalkCanvas;
    public CanvasGroup canvasGroup;
    public List<string> dialogueList1;
    public List<string> dialogueList2;
    private int currentDialogueIndex = 0;

    public float fadeDuration = 0.5f;
    public float initialDelay = 2.0f; 
    public string nextSceneName;

    private bool waitForClick = false;
    public AudioSource ButtonClickSound;
    public AudioSource BGM;
   
    private void Start()
    {
        
        Cursor.visible = true;
        canvasGroup.alpha = 1f;
        if (dialogueList1.Count > 0 && !PlayerData.isEnglish)
        {
            text.text = "";
            StartCoroutine(InitialDelayCoroutine1());
        }

        if (dialogueList1.Count > 0 && PlayerData.isEnglish)
        {
            text.text = "";
            StartCoroutine(InitialDelayCoroutine2());
        }
    }
   
    public void Skip()
    {
       // �ε��� -> 2DEnterScene �̵� ���� 12.15
    }
    private IEnumerator InitialDelayCoroutine1()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(TypingCoroutine1());
    }

    private IEnumerator InitialDelayCoroutine2()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(TypingCoroutine2());
    }

    private IEnumerator TypingCoroutine1()
    {
        while (currentDialogueIndex < dialogueList1.Count)
        {
            string dialogue = dialogueList1[currentDialogueIndex];
            for (int i = 0; i <= dialogue.Length; ++i)
            {
                text.text = dialogue.Substring(0, i);
                yield return new WaitForSeconds(0.0001f);
            }

            waitForClick = true; 
            while (waitForClick)
            {
               
                if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    ButtonClickSound.Play();
                    
                    waitForClick = false;
                    break;

                }
                yield return null;
            }

            currentDialogueIndex++;

            if (currentDialogueIndex >= dialogueList1.Count)
            {
                StartCoroutine(FadeOutAndLoadScene());
                yield break;
            }

            text.text = "";

            yield return null;
        }
    }

    private IEnumerator TypingCoroutine2()
    {
        while (currentDialogueIndex < dialogueList2.Count)
        {
            string dialogue = dialogueList2[currentDialogueIndex];
            for (int i = 0; i <= dialogue.Length; ++i)
            {
                text.text = dialogue.Substring(0, i);
                yield return new WaitForSeconds(0.0001f);
            }

            waitForClick = true;
            while (waitForClick)
            {
               
                if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    ButtonClickSound.Play();
                    
                    waitForClick = false;
                    break;

                }
                yield return null;
            }

            currentDialogueIndex++;

            if (currentDialogueIndex >= dialogueList2.Count)
            {
                StartCoroutine(FadeOutAndLoadScene());
                yield break;
            }

            text.text = "";

            yield return null;
        }
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        float elapsedTime = 0f;
       
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
       
        canvasGroup.alpha = 0f;
        Cursor.visible = false;
        BGM.Stop();
        StartScene();

    }
    void StartScene()
    {
        // �ε��� -> 2DEnterScene �̵� ���� 12.15
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image progressBar;
    [SerializeField] CharacterLoading characterLoading;
    public Material[] newSkyBox;

    private void Start()
    {
        Cursor.visible = false;
        StartCoroutine(LoadScene());
        int ranSkyRange = Random.Range(0, newSkyBox.Length);
        RenderSettings.skybox = newSkyBox[ranSkyRange];
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene_Final");
    }

    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                characterLoading.SetDestination(progressBar.transform.position);

                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                characterLoading.UpdateCharacterMovement(progressBar.fillAmount);

                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
            //else
            //{
            //    characterLoading.SetDestination(new Vector3(1.99000001f, 2.5f, 0f));

            //    if (!characterLoading.HasReachedDestination())
            //    {
            //        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
            //        characterLoading.UpdateCharacterMovement(progressBar.fillAmount);
            //    }

            //    if (progressBar.fillAmount == 1.0f)
            //    {
            //        op.allowSceneActivation = true;
            //        yield break;
            //    }
            //}
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameState : MonoBehaviour
{
    
    public bool isF_1;
    public bool isF_2;
    public bool isF_3;
    public bool isF_4;
    public bool isH_1;
    public bool isH_2;
    public bool isH_3;
    public bool isH_4;
    public bool isH_5;
    public bool isCi;
    public bool isCa_1;
    public bool isCa_2;
    public bool isCa_3;
    public bool isCa_4;
    public bool isCa_5;
    public AudioSource BGM;
    public GameObject LoadingUI;

    public AudioSource ClickSound;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    public void OnMouseDown()
    {
        if (isF_1)
        {
            ClickSound.Play();
           
            SetLoadingUI();
            Invoke("FactoryScene_1Play",3f);
           
            
        }
        else if (isF_2)
        {
            ClickSound.Play();

            SetLoadingUI();
            Invoke("FactoryScene_2Play", 3f);
        }
        else if (isF_3)
        {
            ClickSound.Play();

            SetLoadingUI();
            Invoke("FactoryScene_3Play", 3f);
        }
        else if (isF_4)
        {
            ClickSound.Play();

            SetLoadingUI();
            Invoke("FactoryScene_4Play", 3f);
        }
        else if (isH_1)
        {
            ClickSound.Play();
           
            SetLoadingUI();
            Invoke("HouseScene_1Play",3f);
            
        }
        else if (isH_2)
        {
            ClickSound.Play();

            SetLoadingUI();
            Invoke("HouseScene_2Play", 3f);

        }
        else if (isH_3)
        {
            ClickSound.Play();

            SetLoadingUI();
            Invoke("HouseScene_3Play", 3f);

        }
        else if (isH_4)
        {
            ClickSound.Play();

            SetLoadingUI();
            Invoke("HouseScene_4Play", 3f);

        }
        else if (isH_5)
        {
            ClickSound.Play();

            SetLoadingUI();
            Invoke("HouseScene_5Play", 3f);

        }
        else if (isCi)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CityScenePlay", 3f);
          
            
        }
        else if (isCa_1)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CaveScene_1Play", 3f);
         
        }
        else if (isCa_2)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CaveScene_2Play", 3f);

        }
        else if (isCa_3)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CaveScene_3Play", 3f);

        }
        else if (isCa_4)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CaveScene_4Play", 3f);

        }
        else if (isCa_5)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CaveScene_5Play", 3f);
        }
    }  
    void SetLoadingUI()
    {
        LoadingUI.SetActive(true);
        Cursor.visible = false;
        BGM.Stop();
      
    }
    public void FactoryScene_1Play()
    {
        
        SceneManager.LoadScene("FactoryScene_1");
       
       
    }
    public void FactoryScene_2Play()
    {

        SceneManager.LoadScene("FactoryScene_2");


    }
    public void FactoryScene_3Play()
    {

        SceneManager.LoadScene("FactoryScene_3");


    }
    public void FactoryScene_4Play()
    {

        SceneManager.LoadScene("FactoryScene_4");


    }  
    public void HouseScene_1Play()
    {

        SceneManager.LoadScene("HouseScene1");

    }
    public void HouseScene_2Play()
    {
       
        SceneManager.LoadScene("HouseScene2");

    }
    public void HouseScene_3Play()
    {

        SceneManager.LoadScene("HouseScene3");

    }

    public void HouseScene_4Play()
    {

        SceneManager.LoadScene("HouseScene4");

    }
    public void HouseScene_5Play()
    {

        SceneManager.LoadScene("HouseScene5");

    }
    public void CityScenePlay()
    {
       
        SceneManager.LoadScene("CityScene");

    }
    public void CaveScene_1Play()
    {
        SceneManager.LoadScene("CaveScene_1");
    }
    public void CaveScene_2Play()
    {
        SceneManager.LoadScene("CaveScene_2");
    }
    public void CaveScene_3Play()
    {
        SceneManager.LoadScene("CaveScene_3");
    }
    public void CaveScene_4Play()
    {
        SceneManager.LoadScene("CaveScene_4");
    }
    public void CaveScene_5Play()
    {
        SceneManager.LoadScene("CaveScene_5");
    }
}

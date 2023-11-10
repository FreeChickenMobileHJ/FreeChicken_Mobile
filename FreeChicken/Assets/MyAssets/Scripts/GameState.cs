using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameState : MonoBehaviour
{

    public GameObject menuCanvas;
    public AudioSource BGM;
    public GameObject LoadingUI;

    public AudioSource ClickSound;

  
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && menuCanvas.activeSelf == false)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

       
            Collider2D collider = Physics2D.OverlapPoint(touchPosition);
           
            
            if (collider != null)
            {
               if(collider.name == "Factory_1")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("FactoryScene_1Play", 3f);

                }
               else if(collider.name == "Factory_2")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("FactoryScene_2Play", 3f);

                }
                else if (collider.name == "Factory_3")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("FactoryScene_3Play", 3f);

                }
                else if (collider.name == "Factory_4")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("FactoryScene_4Play", 3f);

                }
                else if (collider.name == "House_1")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("HouseScene_1Play", 3f);

                }
                else if (collider.name == "House_2")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("HouseScene_2Play", 3f);

                }
                else if (collider.name == "House_3")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("HouseScene_3Play", 3f);

                }
                else if (collider.name == "House_4")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("HouseScene_4Play", 3f);

                }
                else if (collider.name == "House_5")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("HouseScene_5Play", 3f);

                }
                else if (collider.name == "City")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("CityScenePlay", 3f);

                }
                else if (collider.name == "Cave_1")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("CaveScene_1Play", 3f);

                }
                else if (collider.name == "Cave_2")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("CaveScene_2Play", 3f);

                }
                else if (collider.name == "Cave_3")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("CaveScene_3Play", 3f);

                }
                else if (collider.name == "Cave_4")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("CaveScene_4Play", 3f);

                }
                else if (collider.name == "Cave_5")
                {
                    ClickSound.Play();

                    SetLoadingUI();
                    Invoke("CaveScene_5Play", 3f);

                }

            }
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

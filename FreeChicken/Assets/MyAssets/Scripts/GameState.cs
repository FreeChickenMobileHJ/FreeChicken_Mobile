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
                if (collider.name == "Factory_1")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("FactoryScene_1");

                }
                else if (collider.name == "Factory_2")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("FactoryScene_2");
                }
                else if (collider.name == "Factory_3")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("FactoryScene_3");
                }
                else if (collider.name == "Factory_4")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("FactoryScene_4");

                }
                else if (collider.name == "House_1")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("HouseScene_1");

                }
                else if (collider.name == "House_2")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("HouseScene_2");
                }
                else if (collider.name == "House_3")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("HouseScene_3");

                }
                else if (collider.name == "House_4")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("HouseScene_4");

                }
                else if (collider.name == "House_5")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("HouseScene_5");


                }
                else if (collider.name == "City")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("CityScene");

                }
                else if (collider.name == "Cave_1")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("CaveScene_1");
                }
                else if (collider.name == "Cave_2")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("CaveScene_2");
                }
                else if (collider.name == "Cave_3")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("CaveScene_3");

                }
                else if (collider.name == "Cave_4")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("CaveScene_4");

                }
                else if (collider.name == "Cave_5")
                {
                    ClickSound.Play();
                    LoadingSceneManager.LoadScene("CaveScene_5");
                }

            }
        }
    }
}

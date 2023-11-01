using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PushButtonDoor_Cave : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;

    public GameObject Player;
    public bool doorSet;  
    public bool isPush;
    public float upSpeed;
    public float downSpeed;

    public GameObject Daddy;
    public bool goDaddy;
    public GameObject target;
    public GameObject DaddyFinish;
    public bool isLast;

    Animator anim;
    public GameObject KissZone;
    public AudioSource TrumpetSound;
    void Start()
    {
        anim = Daddy.GetComponent<Animator>();

        StartCoroutine("CO_isPush");
        StartCoroutine("CO_MoveDoors");
        StartCoroutine("CO_GoDaddy");
    }

    IEnumerator CO_isPush()
    {
        while (true)
        {
            if (isPush)
            {
                MoveDaddy();
                if (!doorSet)
                {
                    Pushing();
                    if (Door1.transform.position.y >= 5f)
                    {
                        doorSet = true;
                        goDaddy = true;
                    }
                }
            }
            yield return null;
        }
    }
    IEnumerator CO_MoveDoors()
    {
        while (true)
        {
            if (!isPush && !doorSet)
            {
                Door1.transform.Translate(Vector3.down * (Time.deltaTime / downSpeed));
                Door2.transform.Translate(Vector3.down * (Time.deltaTime / downSpeed));

                if (Door1.transform.position.y <= 3.384f)
                {
                    doorSet = true;
                    goDaddy = false;
                }
            }

            yield return null;
        }
    }

    IEnumerator CO_GoDaddy()
    {
        while (true)
        {
            if (Daddy.transform.position.x >= target.transform.position.x && isLast && Player.transform.position.x >= target.transform.position.x)
            {
                isLast = false;
                DaddyFinish.SetActive(true);
                Daddy.SetActive(false);
                yield return new WaitForSeconds(2f);
                ButtonFinish();
            }

            yield return null;
        }
    }

    void Pushing()
    {
       if(!doorSet)
        {
            Vector3 translation = new Vector3(0f, 1f, 0f) * Time.deltaTime * upSpeed;
            Door1.transform.Translate(translation);
            Door2.transform.Translate(translation);
        }
    }

    void MoveDaddy()
    {
        if (goDaddy)
        {
            anim.SetBool("isRun", true);
            Daddy.transform.position = Vector3.MoveTowards(Daddy.transform.position, target.transform.position, Time.deltaTime * 2);
        }
    }

    void ButtonFinish()
    {
        DaddyFinish.SetActive(false);
        Daddy.transform.position = new Vector3(Daddy.transform.position.x + 12f, Daddy.transform.position.y, Daddy.transform.position.z-1.2f);
        anim.SetTrigger("Kiss");
    }
   
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TrumpetSound.Play();
            isPush = true;
            doorSet = false;
            goDaddy = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TrumpetSound.Pause();
            isPush = false;
            doorSet = false;
            goDaddy = false;
            anim.SetBool("isRun", false);
        }
    }
}

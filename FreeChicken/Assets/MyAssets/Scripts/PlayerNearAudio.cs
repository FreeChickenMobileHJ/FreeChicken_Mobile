using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearAudio : MonoBehaviour
{
    public GameObject player;
    public AudioSource newAudio;
    public float proximityDistance;
    bool isPlaying = false;
    public float checkInterval = 0.5f;
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(newAudio == null)
        {
            newAudio = GetComponent<AudioSource>();
        }
        newAudio.spatialBlend = 1.0f;  
        newAudio.minDistance = proximityDistance;
        newAudio.maxDistance = proximityDistance * 2f;
        InvokeRepeating("CheckDistance", 0f, checkInterval);
    }

    void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= proximityDistance && !isPlaying)
        {
            PlayAudio();
          
        }
        else if (distance > proximityDistance && isPlaying)
        {
            StopAudio();
        }
    }

    void PlayAudio()
    {
       
        newAudio.Play();
        isPlaying = true;
    }

    void StopAudio()
    {
       
        newAudio.Stop();
        isPlaying = false;
    }

}

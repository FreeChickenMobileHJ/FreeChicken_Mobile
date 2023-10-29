using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    
    public float carSpeed;

    bool attackPlayer;


   
    void Awake()
    {

        Application.targetFrameRate = 30;
    }
    void Start()
    {
        StartCoroutine(Move());


    }
    
  
    IEnumerator Move()
    {
        while (true)
        {
            if (!attackPlayer)
            {
                transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);

            }
            yield return null;
        }
    }
 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackPlayer = true;

            Destroy(this.gameObject, 1f);
        }


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarDestroy")
        {
            
            Destroy(this.gameObject);
            
        }
    }
}

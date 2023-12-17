using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactorySceneSmallManager : MonoBehaviour
{
    GameObject Player;
    public GameObject EggBox;
    public bool isAttack;
    Animator animator;
    public GameObject pos;
    public GameObject obj;
    public ObjectPool objectPool;
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        if (EggBox != null)
        {
            InvokeRepeating("SpawnEggBox", 2f, 3f);
            
        }

        int n = Random.Range(0, 2);
        if(n == 0)
        {
            isAttack = true;
        }
        else if(n == 1)
        {
            isAttack=false;
        }
      
    }

    
    void Update()
    {
        if (isAttack)
        {
            StartCoroutine("Idle");
        }
        else if (!isAttack)
        {
            StartCoroutine("Attack");
            
        }
    }
    void SpawnEggBox()
    {
        obj = objectPool.GetObjectFromPool(pos.transform.position, Quaternion.identity);        
    }
   
        
    IEnumerator Attack()
    {
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(2f);
        isAttack = true;
    }
    IEnumerator Idle()
    {
        animator.SetBool("isAttack", false);
        yield return new WaitForSeconds(2f);
        isAttack = false;
    }
}

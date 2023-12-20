using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] prefab;
    public int poolSize;

    private List<GameObject> objectPool;
    GameObject go;
    void Start()
    {
        InitializeObjectPool();
    }

    void InitializeObjectPool()
    {
        objectPool = new List<GameObject>();
        if(prefab.Length <= 1)
        {
            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(prefab[0], Vector3.zero, Quaternion.identity);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
        }
        else
        {
            for(int i=0;i<prefab.Length;i++)
            {
                for (int j = 0; j < poolSize; j++)
                {
                  
                    GameObject obj = Instantiate(prefab[i], Vector3.zero, Quaternion.identity);
                    obj.SetActive(false);
                    objectPool.Add(obj);
                }
            }
        }
      
    }

    public GameObject GetObjectFromPool(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(go, position, rotation);
        objectPool.Add(newObj);
        return newObj;
    }
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);

    }
}

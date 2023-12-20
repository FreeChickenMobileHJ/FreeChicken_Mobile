using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    public ObjectPool objectPool;
    //public GameObject[] prefab;
    BoxCollider area;
    public int cnt;
    CaveScenePlayer player;

    void Start()
    {
        area = GetComponent<BoxCollider>();
        player = GameObject.Find("CaveCharacter").GetComponent<CaveScenePlayer>();
    }

    void Update()
    {
        if (player.isfallingBook)
        {           
            for (int i = 0; i < cnt; ++i)
            {
                StartCoroutine(Spawn());
            }
            player.isfallingBook = false;
        }       
    }

    IEnumerator Spawn()
    {
        Vector3 pos = GetRandomPos();
        GameObject go = objectPool.GetObjectFromPool(pos, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        objectPool.ReturnObjectToPool(go);
    }

    Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;

        float posX = basePos.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePos.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePos.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        Vector3 pos = area.center + spawnPos;
        return pos;
    }
}

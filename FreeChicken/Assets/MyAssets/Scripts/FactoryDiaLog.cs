using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryDiaLog : MonoBehaviour
{
    
    public string[] sentences;
    public bool isCnt;
    private void Start()
    {
        StartCoroutine(Set());
    }
    IEnumerator Set()
    {
        while (true)
        {
            if (!isCnt)
            {
                FactoryUIManager.instance.OndiaLog(sentences);
            }
            isCnt = true;

            yield return null;
        }
    }
  
}

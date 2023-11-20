using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DeadCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static int count;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine("ChkDeathCnt");
    }
    IEnumerator ChkDeathCnt()
    {
        while (true)
        {
            text.text = "Count : " + count.ToString();
            yield return null;
        }
        
    }
}

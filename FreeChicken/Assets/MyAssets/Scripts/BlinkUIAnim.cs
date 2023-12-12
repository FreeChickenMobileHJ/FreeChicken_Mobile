using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BlinkUIAnim : MonoBehaviour
{
    float time;
    public GameObject nearNpc;

    void Update()
    {
        if(time<0.5f)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1 - time);
           
        }
        else
        {
            GetComponent<Image>().color = new Color(1, 1, 1, time);
            if (time>1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }
}

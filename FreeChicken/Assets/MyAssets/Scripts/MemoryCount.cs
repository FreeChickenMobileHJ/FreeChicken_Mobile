using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MemoryCount : MonoBehaviour
{
    public TextMeshProUGUI text;
 
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = Color.red;
      
    }
    public void MemCntChange(int n,int fin)
    {
        if (n <= fin)
        {
            text.text = n.ToString();
        }
    }
 
}

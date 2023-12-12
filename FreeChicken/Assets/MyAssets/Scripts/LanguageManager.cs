using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;
    public bool isKo;
    public bool isEn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 이 객체는 다음 씬으로 전달될 것이므로 파괴되지 않도록 설정합니다.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLanguage(bool ko, bool en)
    {
        isKo = ko;
        isEn = en;
        PlayerPrefs.SetInt("IsKo", isKo ? 1 : 0);
        PlayerPrefs.SetInt("IsEn", isEn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadLanguage()
    {
        isKo = PlayerPrefs.GetInt("IsKo", 0) == 1;
        isEn = PlayerPrefs.GetInt("IsEn", 0) == 1;
    }
}

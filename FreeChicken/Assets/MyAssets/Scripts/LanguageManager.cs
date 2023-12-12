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
            DontDestroyOnLoad(gameObject); // �� ��ü�� ���� ������ ���޵� ���̹Ƿ� �ı����� �ʵ��� �����մϴ�.
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

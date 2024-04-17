using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    AudioSource bgm;
    void Start()
    {
        // PlayerPrefs���� ����� ���¸� �ҷ��ͼ� ����
        if (PlayerPrefs.GetInt("IsMusicOn", 0) == 0)
            OffMusic();

        else
            OnMusic();
    }

    public void OnMusic()
    {
        bgm.Stop();
        bgm.loop = false;
        PlayerPrefs.SetInt("IsMusicOn", 1);  // PlayerPrefs�� IsMusicOn���� 1�� �����Ͽ� ������ ���� ���¸� ����
        UIManager.Instance.offMusicobject();
    }

    public void OffMusic()
    {
        bgm.Play();
        bgm.loop = true;
        PlayerPrefs.SetInt("IsMusicOn", 0); // PlayerPrefs�� IsMusicOn���� 0�� �����Ͽ� ������ ���� ���¸� ����
        UIManager.Instance.onMusicobject();
    }
}

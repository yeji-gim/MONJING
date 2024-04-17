using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    AudioSource bgm;
    void Start()
    {
        // PlayerPrefs에서 저장된 상태를 불러와서 적용
        if (PlayerPrefs.GetInt("IsMusicOn", 0) == 0)
            OffMusic();

        else
            OnMusic();
    }

    public void OnMusic()
    {
        bgm.Stop();
        bgm.loop = false;
        PlayerPrefs.SetInt("IsMusicOn", 1);  // PlayerPrefs에 IsMusicOn값을 1로 설정하여 음악이 켜진 상태를 저장
        UIManager.Instance.offMusicobject();
    }

    public void OffMusic()
    {
        bgm.Play();
        bgm.loop = true;
        PlayerPrefs.SetInt("IsMusicOn", 0); // PlayerPrefs에 IsMusicOn값을 0로 설정하여 음악이 꺼진 상태를 저장
        UIManager.Instance.onMusicobject();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class blinkText : MonoBehaviour
{
    public TMP_Text textComponent;
    public float blinkInterval = 0.5f; 

    private void Start()
    {
        StartCoroutine(BlinkCoroutine());
    }


    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            textComponent.color = (textComponent.color == Color.white) ? Color.clear : Color.white; // color 값을 변경해 깜빡거리는 것처럼 보이기

            yield return new WaitForSeconds(blinkInterval); // 깜빡이는 주기 설정
        }
    }
}

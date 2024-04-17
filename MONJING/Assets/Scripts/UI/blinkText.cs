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
            textComponent.color = (textComponent.color == Color.white) ? Color.clear : Color.white; // color ���� ������ �����Ÿ��� ��ó�� ���̱�

            yield return new WaitForSeconds(blinkInterval); // �����̴� �ֱ� ����
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowGenrator : MonoBehaviour
{
    [SerializeField] private float initialArrowLength = 1f; // �ʱ� ����
    [SerializeField] private float finalArrowLength = 3f; // ���� ����
    [SerializeField] private float duration = 3f; // �ִϸ��̼� ���� �ð�

    private LineRenderer lineRenderer; 
    private float startTime;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lineRenderer.positionCount = 2; // ���� ���� ����
        lineRenderer.SetPosition(0, transform.position); // ù��° �� ��ġ ����
        lineRenderer.SetPosition(1, transform.position + Vector3.down * initialArrowLength); // �ι�° �� ��ġ ����

        startTime = Time.time;
    }

    private void Update()
    {
        float progress = (Time.time - startTime) / duration; // �ִϸ��̼� ����� ���
        float arrowLength = Mathf.Lerp(initialArrowLength, finalArrowLength, progress); // �ʱ� ���̿� ���� ���� ������ ���� progess�� ����

        // arrow ���� ������Ʈ
        lineRenderer.SetPosition(1, transform.position + Vector3.down * arrowLength); // �ι�° ���� ��ġ�� ������Ʈ

        // �ִϸ��̼� �ʱ�ȭ
        if (progress >= 1.1f) 
            startTime = Time.time;
    }
}

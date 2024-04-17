using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowGenrator : MonoBehaviour
{
    [SerializeField] private float initialArrowLength = 1f; // 초기 길이
    [SerializeField] private float finalArrowLength = 3f; // 최종 길이
    [SerializeField] private float duration = 3f; // 애니메이션 지속 시간

    private LineRenderer lineRenderer; 
    private float startTime;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lineRenderer.positionCount = 2; // 점의 개수 설정
        lineRenderer.SetPosition(0, transform.position); // 첫번째 점 위치 설정
        lineRenderer.SetPosition(1, transform.position + Vector3.down * initialArrowLength); // 두번째 점 위치 설정

        startTime = Time.time;
    }

    private void Update()
    {
        float progress = (Time.time - startTime) / duration; // 애니메이션 진행률 계산
        float arrowLength = Mathf.Lerp(initialArrowLength, finalArrowLength, progress); // 초기 길이와 최종 길이 사이의 값을 progess로 보간

        // arrow 길이 업데이트
        lineRenderer.SetPosition(1, transform.position + Vector3.down * arrowLength); // 두번째 점의 위치를 업데이트

        // 애니메이션 초기화
        if (progress >= 1.1f) 
            startTime = Time.time;
    }
}

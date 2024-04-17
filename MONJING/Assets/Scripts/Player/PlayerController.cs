using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float power = 10f;
    [SerializeField] private float maxDrag = 1f;
    [SerializeField] private float maxDistance = 1f;
    [SerializeField] private Rigidbody2D rb;
    LineRenderer lr;
    Touch touch;
    Vector3 dragStartPos;
    bool characterPressed = false;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        if (lr != null) 
            lr.positionCount = 0;
    }
    void Update()
    {
        // 터치 입력 관리
        if(Input.touchCount > 0 && !EnemyManager.isGameOver)
        {
            touch = Input.GetTouch(0); // 터치 입력을 가져온다.
            switch(touch.phase)
            {
                case TouchPhase.Began: // 터치가 시작되었을때
                    if (isCharacterPressed()) // 플레이어가 누른 상태인지 체크
                    {
                        UIManager.Instance.offPrologue();
                        UIManager.Instance.setSitPlayer();
                        UIManager.Instance.arrow.SetActive(false);
                        characterPressed = true;
                        DragStart();
                    }
                    break;
                case TouchPhase.Moved: // 터치가 이동 중일 때
                    if (characterPressed)
                    {
                        Dragging();
                    }
                    break;
                case TouchPhase.Ended: // 터치가 끝났을 때
                    if (characterPressed)
                    {
                        characterPressed = false;
                        DragRelase();
                        UIManager.Instance.setStandPlayer();
                    }
                    break;
            }
        }
  
    }
    bool isCharacterPressed() 
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void DragStart()
    {
        if (lr != null)
        {
            // 터치한 화면 좌표를 월드 좌표로 변환하여 드래그 시작 지점 위치 설정
            dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
            dragStartPos.z = 0f; // 2D 게임이므로 z좌표를 0으로 설정
            lr.positionCount = 1; // 시작점만 설정하므로 positionCount는 1로 설정
            lr.SetPosition(0, dragStartPos); // 드래그 시작 지점 설정
        }
    }

    void Dragging()
    {
        if (lr != null)
        {
            Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
            draggingPos.z = 0f; // 2D 게임이므로 z좌표를 0으로 설정

            Vector3 pointOffset = draggingPos - dragStartPos; // 시작점과 현재 터치 지점간의 거리르 계산
            var direction = pointOffset.normalized; 

            var distance = Mathf.Min(maxDistance, pointOffset.magnitude); // 드래그 거리를 최대 거리 이내로 설정

            Vector3 endPoint = transform.position + direction * distance; // 현재 오브젝트 위치에서 방향과 거리르 곱한 값으로 계산
            lr.positionCount = 2; 
            lr.SetPosition(1, endPoint); // 두번째 점의 위치 설정
        }
    }

    void DragRelase()
    {
        if (lr != null)
        {
            lr.positionCount = 0; // 드래그가 끝났으니까 점의 개수를 0으로 설정하여 라인 비활성화

            Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
            dragReleasePos.z = 0f;

            Vector3 force = dragStartPos - dragReleasePos; // 드래그 시작지점과 종료 지점사이의 벡터
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power; // 힘의 크기 설정
            rb.AddForce(clampedForce, ForceMode2D.Impulse); // 드래그한 방향으로 플레이어가 움직임
        }
    }
}

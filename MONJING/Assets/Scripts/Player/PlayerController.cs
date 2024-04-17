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
        // ��ġ �Է� ����
        if(Input.touchCount > 0 && !EnemyManager.isGameOver)
        {
            touch = Input.GetTouch(0); // ��ġ �Է��� �����´�.
            switch(touch.phase)
            {
                case TouchPhase.Began: // ��ġ�� ���۵Ǿ�����
                    if (isCharacterPressed()) // �÷��̾ ���� �������� üũ
                    {
                        UIManager.Instance.offPrologue();
                        UIManager.Instance.setSitPlayer();
                        UIManager.Instance.arrow.SetActive(false);
                        characterPressed = true;
                        DragStart();
                    }
                    break;
                case TouchPhase.Moved: // ��ġ�� �̵� ���� ��
                    if (characterPressed)
                    {
                        Dragging();
                    }
                    break;
                case TouchPhase.Ended: // ��ġ�� ������ ��
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
            // ��ġ�� ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ�Ͽ� �巡�� ���� ���� ��ġ ����
            dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
            dragStartPos.z = 0f; // 2D �����̹Ƿ� z��ǥ�� 0���� ����
            lr.positionCount = 1; // �������� �����ϹǷ� positionCount�� 1�� ����
            lr.SetPosition(0, dragStartPos); // �巡�� ���� ���� ����
        }
    }

    void Dragging()
    {
        if (lr != null)
        {
            Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
            draggingPos.z = 0f; // 2D �����̹Ƿ� z��ǥ�� 0���� ����

            Vector3 pointOffset = draggingPos - dragStartPos; // �������� ���� ��ġ �������� �Ÿ��� ���
            var direction = pointOffset.normalized; 

            var distance = Mathf.Min(maxDistance, pointOffset.magnitude); // �巡�� �Ÿ��� �ִ� �Ÿ� �̳��� ����

            Vector3 endPoint = transform.position + direction * distance; // ���� ������Ʈ ��ġ���� ����� �Ÿ��� ���� ������ ���
            lr.positionCount = 2; 
            lr.SetPosition(1, endPoint); // �ι�° ���� ��ġ ����
        }
    }

    void DragRelase()
    {
        if (lr != null)
        {
            lr.positionCount = 0; // �巡�װ� �������ϱ� ���� ������ 0���� �����Ͽ� ���� ��Ȱ��ȭ

            Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
            dragReleasePos.z = 0f;

            Vector3 force = dragStartPos - dragReleasePos; // �巡�� ���������� ���� ���������� ����
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power; // ���� ũ�� ����
            rb.AddForce(clampedForce, ForceMode2D.Impulse); // �巡���� �������� �÷��̾ ������
        }
    }
}

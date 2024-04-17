using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float changeDirectionInterval = 2f;
    private void Start()
    {
        InvokeRepeating("changeDirection", 0.5f, changeDirectionInterval);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        clampPosition();
    }

    private void changeDirection()
    {
        // RandomSeed�� ����
        float randomSeed = Random.Range(0f, 100f);
        Random.InitState((int)randomSeed);
       
        // ���� ����
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector3 newDirection = new Vector3(randomX, randomY, 0f).normalized;

        // ���� ������ ����
        transform.right = newDirection;
    }

    private void clampPosition()
    {

        float clampedX = Mathf.Clamp(transform.position.x, -1.5f, 1.5f);
        float clampedY = Mathf.Clamp(transform.position.y, -1f, 2.3f);

        // ���ο� ��ġ�� ����
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}

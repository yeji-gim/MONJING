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
        // RandomSeed값 변경
        float randomSeed = Random.Range(0f, 100f);
        Random.InitState((int)randomSeed);
       
        // 랜덤 방향
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector3 newDirection = new Vector3(randomX, randomY, 0f).normalized;

        // 현재 방향을 변경
        transform.right = newDirection;
    }

    private void clampPosition()
    {

        float clampedX = Mathf.Clamp(transform.position.x, -1.5f, 1.5f);
        float clampedY = Mathf.Clamp(transform.position.y, -1f, 2.3f);

        // 새로운 위치로 설정
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}

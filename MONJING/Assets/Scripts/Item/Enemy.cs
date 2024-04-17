using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    float fallSpeed; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.GameOverPanel();
            Time.timeScale = 0f;
            EnemyManager.isGameOver = true;
        }

        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    public void fallEnemy()
    {
        rb.gravityScale = 1;
        // fallSpped 낙하 속도 설정
        if (UIManager.score > 10) fallSpeed = 2f;
        else if (UIManager.score > 30) fallSpeed = 4f;
        else if (UIManager.score > 50) fallSpeed = 6f;
        rb.velocity = new Vector2(0, -fallSpeed); // velocity의 y축 값을 변경해 낙하되도록 설정
    }

}

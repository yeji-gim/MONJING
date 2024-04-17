using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.IncreaseScore(1);
            coinManager.Instance.SpawnCoinRoutine();         
            Destroy(gameObject,0.1f);    
        }
    }
}

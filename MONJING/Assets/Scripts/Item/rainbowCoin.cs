using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbowCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinManager.Instance.SpawnRainbowCoinRoutine();
            UIManager.Instance.IncreaseScore(3);        
            Destroy(gameObject, 0.1f);
        }
    }
}

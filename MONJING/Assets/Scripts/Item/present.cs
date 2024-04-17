using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class present : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            presentManager.Instance.presentreset();
            UIManager.Instance.IncreaseScore(5);
            float randomValue = Random.value;

            if (randomValue <= 0.97f)
                presentManager.Instance.presenton();
            else
                presentManager.Instance.presentreset();

            Destroy(gameObject);
        }
    }
}




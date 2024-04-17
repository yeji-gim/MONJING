using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    [SerializeField] private GameObject EnemyPrefab;
    public static bool isGameOver = false;
    void Start()
    {
        StartCoroutine(spawnEnemyTime());
    }

    public IEnumerator spawnEnemyTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            if (UIManager.score > 10)
            {
                yield return new WaitForSeconds(Random.Range(2f, 4f));
                Vector2 spawnPosition = new Vector2(Random.Range(-1.8f, 1.8f), 2.7f);
                spawnEnemy(spawnPosition, 3f);
            }
            if (UIManager.score > 50)
            {
                yield return new WaitForSeconds(Random.Range(2f, 3f));
                Vector2 spawnPosition2 = new Vector2(Random.Range(-1.8f, 1.8f), 2.7f);
                spawnEnemy(spawnPosition2, 1.3f);
            }

        }
    }

    private void spawnEnemy(Vector2 position, float time)
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, position, Quaternion.identity);
        Animator enemyAnimator = newEnemy.GetComponent<Animator>();
        StartCoroutine(animator(enemyAnimator, time));
    }

    private IEnumerator animator(Animator animator, float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetTrigger("IsShaking");
    }
}

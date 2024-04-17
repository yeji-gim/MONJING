using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinManager : MonoBehaviour
{
    public static coinManager Instance { get; private set; }
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

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject rainbowPrefab;
    GameObject newCoin;
    GameObject rainbowCoin;
    public void SpawnCoinRoutine()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-1.1f, 1.1f), Random.Range(-1f, 2.3f));
        StartCoroutine(spawnDelayTime(spawnPosition));
    }

    public void SpawnRainbowCoinRoutine()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-1.1f, 1.1f), Random.Range(-1f, 2.3f));
        StartCoroutine(rainbowspawnDelayTime(spawnPosition));
    }

    IEnumerator rainbowspawnDelayTime(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(5f);
        if (!IsExists("rainbowStar"))
            rainbowCoin = Instantiate(rainbowPrefab, spawnPosition, Quaternion.identity);
    }
    IEnumerator spawnDelayTime(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        if(!IsExists("yellowStar"))
            newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
    bool IsExists(string tagname)
    {
        GameObject gameObject = GameObject.FindWithTag(tagname);
        return gameObject != null;
    }
}
   


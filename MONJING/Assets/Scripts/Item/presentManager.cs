using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class presentManager : MonoBehaviour
{
    public static presentManager Instance { get; private set; }

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

    public GameObject presnetPrefab;
    [Header("sit")]
    public List<GameObject> sitpresnet;
    [Header("stand")]
    public List<GameObject> standpresnet;

    private void Start()
    {
        StartCoroutine(SpawnpresentRoutine());
    }
    public IEnumerator SpawnpresentRoutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 6f));

            Vector2 spawnPosition = new Vector2(Random.Range(-1.8f, 1.8f), Random.Range(-2.2f,2.2f));
            if (UIManager.score > 10)
            {
                yield return new WaitForSeconds(Random.Range(3f, 6f));
                GameObject newPresent = Instantiate(presnetPrefab, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(5f);
                Destroy(newPresent);

            }
        }
    }

    public void presenton()
    {
        float randomSeed = Random.Range(0f, 15f);
        Random.InitState((int)randomSeed);
        int a = Random.Range(0, sitpresnet.Count);
        sitpresnet[a].SetActive(true);
        standpresnet[a].SetActive(true);
    }

    public void presentreset()
    {
        for (int i = 0; i < sitpresnet.Count; i++)
        {
            sitpresnet[i].SetActive(false);
            standpresnet[i].SetActive(false);
        }
    }
}

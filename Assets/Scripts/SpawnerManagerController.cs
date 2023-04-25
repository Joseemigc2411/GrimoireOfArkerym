using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagerController : MonoBehaviour
{
    public GameObject[] spawnerList;
    public SpawnPointController spawnPointController;
    public float spawnPhantomCounter = 2;
    private bool spawnReady;
    private int numEnemiesAlive = 0;
    
// Start is called before the first frame update
    void Start()
    {
        spawnReady = true;
    }

// Update is called once per frame
    void Update()
    {
        if (spawnPhantomCounter > 0 && spawnReady && numEnemiesAlive == 0)
        {
            spawnReady = false;
            int randomValue = Random.Range(0, 4);
            spawnPointController = spawnerList[randomValue].GetComponentInChildren<SpawnPointController>();
            spawnPointController.spawnEnemy();
            ++numEnemiesAlive;
            --spawnPhantomCounter;
            StartCoroutine(spawnerTime());
        }
    }

    IEnumerator spawnerTime()
    {
        while (numEnemiesAlive > 0) {
            yield return null;
        }
        yield return new WaitForSeconds(5);
        spawnReady = true;
    }

    public void EnemyKilled()
    {
        --numEnemiesAlive;
    }
}

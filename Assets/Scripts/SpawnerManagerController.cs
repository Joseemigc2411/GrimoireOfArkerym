using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagerController : MonoBehaviour
{
    public GameObject[] spawnerList;
    public SpawnPointController spawnPointController;
    public float spawnPhantomCounter = 2;
    private bool spawnReady;
    
    
// Start is called before the first frame update
    void Start()
    {
        spawnReady = true;
    }

// Update is called once per frame
    void Update()
    {
        if (spawnPhantomCounter > 0 && spawnReady)
        {
            spawnReady = false;
            int randomValue = Random.Range(0, 4);
            spawnPointController = spawnerList[randomValue].GetComponentInChildren<SpawnPointController>();
            spawnPointController.spawnEnemy();
            --spawnPhantomCounter;
            StartCoroutine(spawnerTime());
        }
    }

    IEnumerator spawnerTime()
    {
       
        yield return new WaitForSeconds(15);
        spawnReady = true;
    }

    
}

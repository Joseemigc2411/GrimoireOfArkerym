using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using System;


public class SpawnPointController : MonoBehaviour
{
    public GameObject phantom;

    private void Start()
    {
        
    }

    public void spawnEnemy()
    {
        GameObject newEnemy = Instantiate(phantom, transform.position, Quaternion.identity);
    }
    
    
}

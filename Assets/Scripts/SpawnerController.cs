using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Variables")]
    public float spawnRadius;
    bool spawnReady;

    [Header("Componentes")]
    private Transform playerTransform;
    
    [Header("Objetos")]
    public GameObject enemy;
    public GameObject validZoneDetector;

    // Start is called before the first frame update
    void Start()
    {

        spawnReady = true;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //Tomamos la posición del jugador para que sea el centro de nuestro radio

        transform.position = playerTransform.position;

        Vector2 randomPoint = GenerateRandomPointAtDistance(transform.position, spawnRadius);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;

        if (spawnReady)
        {   
            Vector2 randomPoint = GenerateRandomPointAtDistance(transform.position, spawnRadius);

        }
        
    }

    Vector2 GenerateRandomPointAtDistance(Vector2 origin, float distance)
    {
        // Genera un ángulo aleatorio en radianes
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);

        // Calcula las coordenadas (x, y) del nuevo punto
        float x = origin.x + distance * Mathf.Cos(randomAngle);
        float y = origin.y + distance * Mathf.Sin(randomAngle);

        return new Vector2(x, y);
    }



}

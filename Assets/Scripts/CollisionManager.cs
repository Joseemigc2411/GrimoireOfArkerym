using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    
   
    public int damage = 1;

    public EnemyController enemyController;

    public CharacterController playerController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        playerController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        //Se tuvo que poner así porque el script perdía la referencia del jugador por algún motivo desconocido.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Sword") && enemyController != null)
        {
            Debug.Log("He entrado al bucle");
            enemyController.TakeDamage(damage, playerController.elementoActual);
        }
    }
}

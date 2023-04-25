using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    
   
    public int damage = 1;

    public EnemyController enemyController;
    public PhantomController phantomController;

    public CharacterController playerController;

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        //Se tuvo que poner así porque el script perdía la referencia del jugador por algún motivo desconocido.
        
       identifyEnemyType();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Sword") && enemyController != null)
        {
            Debug.Log("He entrado al bucle");
            enemyController.TakeDamage(damage, playerController.elementoActual);
        }
        else if (other != null && other.CompareTag("Sword") && phantomController != null)
        {
            Debug.Log("Soy un fantasma y he detectado una colision con la espada del juagdor");
            phantomController.TakeDamage(damage, playerController.elementoActual);
        }
    }

    void identifyEnemyType()
    {
        if (transform.parent.CompareTag("Phantom"))
        {
            phantomController = transform.parent.GetComponent<PhantomController>();
            enemyController = null;
        }
        else if(transform.parent.CompareTag("LandEnemy"))
        {
            enemyController = transform.parent.GetComponent<EnemyController>();
            phantomController = null;
        }
    }
}

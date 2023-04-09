using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Sword";
    [SerializeField]
    private int damage = 1;

    private EnemyController enemyController;

    public CharacterController playerController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        playerController = GetComponent<CharacterController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            enemyController.TakeDamage(damage, playerController.elementoActual); //Le pasamos al enemigo el daño que le hará el golpe y el hechizo activo del jugador. 
        }
    }
}

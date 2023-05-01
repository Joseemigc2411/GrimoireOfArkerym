using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Transform playerTransform;
    private Animator animator;
    private CharacterController player;
    int HP = 1, actualSpellEnemy;
    
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        #region Asignación de componentes y objetos
        
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            
        #endregion
        
        actualSpellEnemy = UnityEngine.Random.Range(0, 4); //Valor random para la capa del Animator
        animator.SetLayerWeight(actualSpellEnemy, 1); //Seteamos el peso en el animator a la capa generada aleatoriamente
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        MoveTowardsPlayer(directionToPlayer);
        
    }
    
    void MoveTowardsPlayer(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;         
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Me destruyo porque he tocado al jugador.");
            player.TakeDamage();
            Destroy(gameObject);
        }
        
        
    }
    
    public void TakeDamage(int damage, int activeSpell)
    {
        
        if (activeSpell == actualSpellEnemy)
            HP -= damage;

        if (HP <= 0)
        {
            Debug.Log("Me destruyo porque el jugador me ha matado");
            Destroy(gameObject);
        }       
    }
}

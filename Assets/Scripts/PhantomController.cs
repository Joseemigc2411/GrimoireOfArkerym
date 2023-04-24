using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Transform playerTransform;
    private Animator animator;
    
    int HP = 1, actualSpellEnemy;
    
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        actualSpellEnemy = UnityEngine.Random.Range(0, 4); //Valor random para la capa del Animator
        animator.SetLayerWeight(actualSpellEnemy, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        MoveTowardsPlayer(directionToPlayer);
        
    }
    
    void MoveTowardsPlayer(Vector2 direction)
    {
        rb.velocity = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y)) * (speed); 
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }

    public void TakeDamage(int damage, int activeSpell)
    {
        if (activeSpell == actualSpellEnemy)
            HP -= damage;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }       
    }
}

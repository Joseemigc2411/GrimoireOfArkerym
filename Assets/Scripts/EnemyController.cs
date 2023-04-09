using System;
using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;
    private Animator animator;

    public float speed = 3.0f;
    public float detectMultiplier;
    public float chaseRadius = 5.0f;
    private float nextDirectionChangeTime;
    public string obstacleTag;

    private string[] elementos = { "RedLayer", "LightLayer", "PurpleLayer", "BlueLayer" };

    public int HP = 2;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nextDirectionChangeTime = Time.time;
    }

    void Update()
    {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseRadius)
        {
            MoveTowardsPlayer(directionToPlayer);
        }
        else
        {
            MoveRandomly();
        }

        #region Animator

        animator.SetFloat("X Direction", rb.velocity.x);
        animator.SetFloat("Y Direction", rb.velocity.y);

        #endregion
    }


    #region Movimiento

    void MoveTowardsPlayer(Vector2 direction)
    {
        rb.velocity = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y)) * detectMultiplier * speed; //Cuando el enemigo detecta al jugador, su velocidad se duplica
        
    }

    void MoveRandomly()
    {
        // Ajusta este valor para cambiar la frecuencia de cambio de dirección

        float changeDirectionInterval = 2f;

        if (Time.time >= nextDirectionChangeTime)
        {
            int randomX = 0;
            int randomY = 0;
            bool horizontalMove = UnityEngine.Random.value < 0.5;

            if (horizontalMove)
            {
                randomX = UnityEngine.Random.Range(-1, 2);
            }
            else
            {
                randomY = UnityEngine.Random.Range(-1, 2);
            }

            rb.velocity = new Vector2(randomX, randomY) * speed;
            
            nextDirectionChangeTime = Time.time + changeDirectionInterval;
        }
    }

    void RecalculateDirectionOnCollision()
    {
        int randomX = 0;
        int randomY = 0;
        bool horizontalMove = UnityEngine.Random.value < 0.5;

        if (horizontalMove)
        {
            randomX = UnityEngine.Random.Range(-1, 2);
        }
        else
        {
            randomY = UnityEngine.Random.Range(-1, 2);
        }

        rb.velocity = new Vector2(randomX, randomY) * speed;
    }

    #endregion

    public void TakeDamage(int damage, int activeSpell)
    {
        HP -= damage;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) //Si choca contra un collider del escenario, recalcula la posición
        {
            RecalculateDirectionOnCollision();           
        }

        else if (collision.gameObject.CompareTag("Player")) //Si choca contra el jugador, le hace daño
        {
            Destroy(this.gameObject);
        }
      
    }

    


}

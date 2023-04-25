using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D body; 
    Animator animator;
    public Scrollbar castBar, healthBar; //Variables de las scrollbars del canvas
    public GameObject[] AttackColliders; //Array de Game Objects correspondientes a los colliders de ataque

    float horizontal;
    float vertical;
    public float attackDistance;

    private Vector2 lastMovementInput;
    private KeyCode lastKeyInput;
    KeyCode[] keys = new KeyCode[] { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow };

    public float runSpeed;
    public float cooldownState; //Cooldown de cambio de estado, evita spammeo
    private float cooldownStateValue; //Representa el valor actual de cooldown
    public float attackColliderTime = .1f; //Lo que dura un collider de ataque
    public float cooldownAttack; //Cooldown de ataque, evita spammeo

    bool swapReady;
    bool attackReady;

    public int dmgLandEnemy;

    public float maxHealth = 100f;
    public float HP;

    // Array de estados
    private string[] elementos = { "RedLayer", "LightLayer", "PurpleLayer", "BlueLayer" };

    public int elementoActual = 0; //Esta variable no solo sirve internamente, sino para que el enemigo compruebe si le están golpeando con su elemento y recibir el daño o no
    

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); //Asigno el Rigidbody2D
        animator = GetComponent<Animator>(); //Asigno el animator
        swapReady = true;
        attackReady = true;
        HP = maxHealth;

        for (int i = 0; i < AttackColliders.Length; i++) //Desactivamos todos los GameObjects que contienen los colliders de ataque direccional
        {
            AttackColliders[i].SetActive(false); 
        }
        
    }

    void Update()
    {

        //Gestor del cooldown de cambio de estado y de la barra del canvas
        #region CooldownManager

        if (cooldownStateValue >= cooldownState) 
        {
            swapReady = true;
        }
        else
        {
            swapReady = false;
            cooldownStateValue += Time.deltaTime;
            cooldownStateValue = Mathf.Clamp(cooldownStateValue, 0.0f, cooldownState);

        }

        castBar.size = cooldownStateValue / cooldownState;
        #endregion 

        //Módulo de gestión de inputs
        #region InputManager

        if (Input.GetKeyDown(KeyCode.Space) && swapReady)
        {
            CambiarEstado();
        }
       
        
            castBar.size = cooldownStateValue / cooldownState;
        
        
        if (Input.GetKeyDown(KeyCode.X) && attackReady)
        {
            animator.SetTrigger("Attack");
            Attack();
            attackReady = false;
            StartCoroutine(AttackCooldown());

        }

      
        horizontal = Input.GetAxisRaw("Horizontal"); //Movimiento en el eje x
        vertical = Input.GetAxisRaw("Vertical"); //Movimiento en el eje y

        if (horizontal != 0 && vertical != 0) //Impide el movimiento diagonal
        {
            horizontal *= 0;
            vertical *= 0;    
        }

       

        foreach (KeyCode key in keys) //Deteccion de ultima tecla presionada que orienta los colliders de ataque  
        {
            if (Input.GetKeyDown(key))
            {
                lastKeyInput = key;         
                break;
            }
        }

        #endregion

        //Actualización de variables del animator
        #region Animator

        if (body.velocity.magnitude == 0)
        {
            animator.SetBool("Walking", false);
           
        } 
        else
        {
            animator.SetBool("Walking", true);
        }

        animator.SetFloat("X Direction", horizontal);
        animator.SetFloat("Y Direction", vertical);

        #endregion

        healthBar.size = HP / maxHealth;
    }

    private void FixedUpdate() //La velocidad del pj se actualiza en un Fixed, que le da más estabilidad
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LandEnemy"))
        {
            TakeDamage(dmgLandEnemy);
        }
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Phantom"))
        {
            Debug.Log("Fantasma colision");
            TakeDamage(dmgLandEnemy);
        }
    }

    #region Funciones de Cambio de Estado

    void CambiarEstado() // Función para cambiar el estado del personaje 
    {
        cooldownStateValue = 0;

        if (elementoActual == elementos.Length - 1) // Esta condición es la que permite volver al color rojo cuando se ha recorrido el array completo
        {       
            elementoActual = 0;
            animator.SetLayerWeight(elementoActual + 3, 0);
        }
        else
        {
            elementoActual++;
            animator.SetLayerWeight(elementoActual - 1, 0);
        }

        
        animator.SetLayerWeight(elementoActual, 1);
        swapReady = false;
        // Debug.Log("Estado actual: " + elementos[elementoActual]);
        // Debug.Log(elementoActual);

    }   

    #endregion

    #region Funciones Ataque

    private void Attack()
    {
        assignCollider();
    }

    
    private void assignCollider() //Con las diferentes condiciones, asegura que se activa el collider correcto
    {
        if(horizontal > 0 || lastKeyInput == KeyCode.D || lastKeyInput == KeyCode.RightArrow)
        {
            StartCoroutine(PerformAttack(AttackColliders[0]));
            
            //Debug.Log("He generado un collider a la derecha");

        }
        else if(vertical > 0 || lastKeyInput == KeyCode.W || lastKeyInput == KeyCode.UpArrow)
        {
            StartCoroutine(PerformAttack(AttackColliders[3]));
           
            //Debug.Log("He generado un collider arriba");
        }
        else if (horizontal < 0 || lastKeyInput == KeyCode.A || lastKeyInput == KeyCode.LeftArrow)
        {
            StartCoroutine(PerformAttack(AttackColliders[2]));
        
            //Debug.Log("He generado un collider a la izquierda");
        }
        else if (vertical < 0 || lastKeyInput == KeyCode.S || lastKeyInput == KeyCode.DownArrow)
        {
            StartCoroutine(PerformAttack(AttackColliders[1]));
            //Debug.Log("He generado un collider abajo");
        }
    }
    
    IEnumerator PerformAttack(GameObject attackCollider)
    {
       
        //Activa el collider de ataque que le hayamos pasado
        attackCollider.SetActive(true);

        //Espera un momento antes de desactivar el collider de ataque
        yield return new WaitForSeconds(attackColliderTime);

        //Desactiva el collider de ataque
        attackCollider.SetActive(false);

       

    }

    IEnumerator AttackCooldown() //Corrutina de cooldown para que no se pueda spammear el cambio de estado.
    {
        yield return new WaitForSeconds(cooldownAttack);
        // Debug.Log("Ready");
        attackReady = true;
    }

    #endregion

    private void TakeDamage(float damage)
    {
        HP -= damage;

        //Emitir sonido de daño y probar a meter algo de feedback extra.
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D body; 
    Animator animator;

    float horizontal;
    float vertical;
   

    public float runSpeed;
    public float cooldownState = 0.2f; //Cooldown de cambio de estado

    bool swapReady;

    // Array de estados
    private string[] elementos = { "RedLayer", "LightLayer", "PurpleLayer", "BlueLayer" };
    private int elementoActual = 0;

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); // Asigno el Rigidbody2D
        animator = GetComponent<Animator>(); // Asigno el animator
        swapReady = true;
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && swapReady)
        {
            CambiarEstado();
            StartCoroutine(StateChange());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Attack");
        }

      
        horizontal = Input.GetAxisRaw("Horizontal"); //Movimiento en el eje x
        vertical = Input.GetAxisRaw("Vertical"); // Movimiento en el eje y

        if (horizontal != 0 && vertical != 0) // Impide el movimiento diagonal
        {
            horizontal *= 0;
            vertical *= 0;    
        } 

        // Módulo de comunicación con el animator
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
        
    }

    
 
    private void FixedUpdate() // El movimiento del pj se actualiza en un Fixed, que le da más estabilidad
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
  

    #region Cambio de Estado

    void CambiarEstado() // Función para cambiar el estado del personaje 
    {

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

    
    IEnumerator StateChange() //Corrutina de cooldown para que no se pueda spammear el cambio de estado.
    {
        yield return new WaitForSeconds(cooldownState);
        // Debug.Log("Ready");
        swapReady = true;
    }

    #endregion

   
}

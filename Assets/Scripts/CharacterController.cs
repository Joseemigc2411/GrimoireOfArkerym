using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;

    float horizontal;
    float vertical;
   

    public float runSpeed = 5f;

    // Array de estados
    private string[] elementos = { "RedLayer", "LightLayer", "PurpleLayer", "BlueLayer" };
    private int elementoActual = 0;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CambiarEstado();
        }

      
        horizontal = Input.GetAxisRaw("Horizontal"); //Movimiento en el eje x
        vertical = Input.GetAxisRaw("Vertical"); // Movimiento en el eje y

        if (horizontal != 0 && vertical != 0) 
        {
            horizontal *= 0;
            vertical *= 0;
            
        } // Impide el movimiento diagonal

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

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed).normalized;
    }

    // Funcion para cambiar el estado del personaje
    void CambiarEstado()
    {
        if (elementoActual == elementos.Length - 1)
        {       
            elementoActual = 0;
            animator.SetLayerWeight(elementoActual + 4, 0);
        }
        else
        {
            elementoActual++; animator.SetLayerWeight(elementoActual - 1, 0);
        }

       
        animator.SetLayerWeight(elementoActual, 1);
        Debug.Log("Estado actual: " + elementos[elementoActual]);
        Debug.Log(elementoActual);
        
    }


}

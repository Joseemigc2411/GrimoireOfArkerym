using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 5f;

    // Array de estados
    private string[] elementos = { "Fuego", "Hielo", "Arcano", "Luz" };
    private int elementoActual = 0;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Cambiar estado al presionar una tecla
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CambiarEstado();
        }

        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= 0;
            vertical *= 0;
        }

    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

    }

    // Función para cambiar el estado del personaje
    void CambiarEstado()
    {
        elementoActual++;
        if (elementoActual >= elementos.Length)
        {
            elementoActual = 0;
        }
        Debug.Log("Estado actual: " + elementos[elementoActual]);
    }
}

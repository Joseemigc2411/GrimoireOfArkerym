using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{

    public bool validPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpawnZone"))
        {
            validPoint = true;
        }
        else
        {
            validPoint = false;
        }

        if (collision.gameObject.CompareTag(null))
        {
            validPoint = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumTrap : MonoBehaviour
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Animator>().enabled = true;
        }
    }
}

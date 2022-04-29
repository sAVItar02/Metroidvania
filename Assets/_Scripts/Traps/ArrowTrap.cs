using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    private Animator animator;

    [SerializeField] GameObject arrow;
    [SerializeField] Transform spawnLocation;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("isInRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isInRange", false);
        }
    }

    public void spawnArrow()
    {
        Instantiate(arrow, spawnLocation.transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();
    }
}

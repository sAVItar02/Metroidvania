using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] float arrowSpeed;
    [SerializeField] int arrowDamage = 10;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject bloodEffect;

    private Rigidbody2D rb;
    private float timeFromSpawn;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * arrowSpeed;
        timeFromSpawn = 0f;
    }

    void Update()
    {
        timeFromSpawn += Time.deltaTime;
        if (timeFromSpawn > lifetime) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            collision.GetComponent<PlayerController>().TakeDamage(arrowDamage);
            Destroy(gameObject);
        }

        if(collision.CompareTag("Ground"))
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

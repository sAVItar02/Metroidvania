using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private float timeFromSpawn;

    [SerializeField] float fireballSpeed;
    [SerializeField] int fireballDamage;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject bloodEffect;
    [SerializeField] float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * fireballSpeed;
        timeFromSpawn = 0f;
    }

    private void Update()
    {
        timeFromSpawn += Time.deltaTime;
        if (timeFromSpawn > lifetime) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        switch (collision.tag)
        {
            case "Skelly":
                collision.GetComponent<SkellyBehaviour>().TakeDamage(fireballDamage);
                break;
            case "Slime":
                collision.GetComponent<SlimeBehaviour>().TakeDamage(fireballDamage);
                break;
            case "FireWorm":
                    break;
        }
                
        /*if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<SkellyBehaviour>().TakeDamage(fireballDamage);
        }*/
    }
}

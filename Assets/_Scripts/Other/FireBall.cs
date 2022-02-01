using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float fireballSpeed;
    [SerializeField] int fireballDamage;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * fireballSpeed;
        lifetime = 0f;
    }

    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 5) { Destroy(gameObject); }
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
        }
                
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<SkellyBehaviour>().TakeDamage(fireballDamage);
        }
    }
}

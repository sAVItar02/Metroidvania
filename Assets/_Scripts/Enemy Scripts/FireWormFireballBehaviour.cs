using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWormFireballBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float timeFromSpawn;

    [SerializeField] float fireballSpeed;
    [SerializeField] int fireballDamage;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject bloodEffect;
    [SerializeField] float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("didHit");
            rb.velocity = Vector2.zero;
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            collision.GetComponent<PlayerController>().TakeDamage(fireballDamage);
        }

        if (collision.CompareTag("Ground"))
        {
            anim.SetTrigger("didHit");
            rb.velocity = Vector2.zero;
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

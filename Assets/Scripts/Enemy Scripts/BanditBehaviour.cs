using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBehaviour : MonoBehaviour
{
    private Animator banditAnimator;

    public int maxHealth = 100;
    int currentHealth;
    void Start()
    {
        banditAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        banditAnimator.SetTrigger("hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        banditAnimator.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackZoneCheck : MonoBehaviour
{
    private Animator anim;
    private SlimeBehaviour slime;

    [SerializeField] float timeBetweenAttacks = 2f;
    
    public bool canSlimeAttack = true;
    public bool isAttacking = false;

    [SerializeField]private float attackTimer;
    void Start()
    {
        slime = GetComponentInParent<SlimeBehaviour>();
        anim = GetComponentInParent<Animator>();
        attackTimer = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer <= 0)
        {
            canSlimeAttack = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerController.Instance.isDead)
        {
            if (canSlimeAttack)
            {
                Attack();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAttack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        attackTimer = timeBetweenAttacks;
        canSlimeAttack = false;
        anim.SetBool("Attack", true);
    }

    void StopAttack()
    {
        anim.SetBool("Attack", false);
    }
}

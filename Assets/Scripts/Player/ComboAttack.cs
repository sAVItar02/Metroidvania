using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] int noOfClicks = 0;
    [SerializeField] float maxComboDelay = 0.9f;
    float lastClickedTime = 0f;

    [Space]
    [Header("Attack Params")]
    [SerializeField] int attackDamage = 20;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if(Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if(Input.GetMouseButtonDown(0))
        {
            lastClickedTime = Time.time;
            noOfClicks++;

            if(noOfClicks == 1)
            {
                playerAnimator.SetBool("Attack1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
    }

    public void Attack()
    {
        //Check for enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<SkellyBehaviour>().TakeDamage(attackDamage);
        }
    }

    //Animation Event Functions
    public void return1()
    {
        if(noOfClicks >= 2)
        {
            playerAnimator.SetBool("Attack2", true);
        } else
        {
            playerAnimator.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }

    public void return2()
    {
        if (noOfClicks >= 3)
        {
            playerAnimator.SetBool("Attack3", true);
        }
        else
        {
            playerAnimator.SetBool("Attack2", false);
            noOfClicks = 0;
        }
    }

    public void return3()
    {
        playerAnimator.SetBool("Attack1", false);
        playerAnimator.SetBool("Attack2", false);
        playerAnimator.SetBool("Attack3", false);
        noOfClicks = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

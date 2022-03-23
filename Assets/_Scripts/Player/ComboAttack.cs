using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    #region
    private Animator playerAnimator;
    [SerializeField] int noOfClicks = 0;
    [SerializeField] float maxComboDelay = 0.9f;
    float lastClickedTime = 0f;

    [Space]
    [Header("Attack Params")]
    [SerializeField] int damageToDeal = 20;
    [SerializeField] int critChance = 10;
    [SerializeField] int maxCritDamage = 40;
    [SerializeField] int minCritDamage = 35;
    [SerializeField] Transform attackPoint;
    public bool canUseSecondary = true;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] public float secondaryAttkTimeDelay = 5f;
    float secondaryAttkTimer;
    [SerializeField] LayerMask enemyLayer;
    [Space]
    [Header("Secondary Attack")]
    [SerializeField] GameObject fireball;
    [SerializeField] Transform firePoint;
    #endregion
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        secondaryAttkTimer = secondaryAttkTimeDelay;
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

        if(Input.GetMouseButtonDown(1) && canUseSecondary)
        {
            playerAnimator.SetTrigger("activateSecondary");
            PlayerController.Instance.isAttacking = true;
            canUseSecondary = false;
            secondaryAttkTimer = secondaryAttkTimeDelay;
        }
        if(secondaryAttkTimer >= 0)
        {
            secondaryAttkTimer -= Time.deltaTime;
        }
        if(secondaryAttkTimer <= 0)
        {
            canUseSecondary = true;
        }
    }

    public void Attack()
    {
        PlayerController.Instance.isAttacking = true;
        //Check for enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        damageToDeal = 20;
        int damageDecider = Random.Range(0, 101);
        if(damageDecider > (100-critChance) && damageDecider <= 100)
        {
            damageToDeal = Random.Range(minCritDamage, maxCritDamage);
        } else
        {
            damageToDeal = Random.Range(19, 24);
        }

        //Damage Enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            switch (enemy.tag)
            {
                case "Skelly":
                    enemy.GetComponent<SkellyBehaviour>().TakeDamage(damageToDeal);
                    break;
                case "Slime":
                    enemy.GetComponent<SlimeBehaviour>().TakeDamage(damageToDeal);
                    break;
                case "FireWorm":
                    enemy.GetComponent<SlimeBehaviour>().TakeDamage(damageToDeal);
                    break;
                    
            }
            
        }
    }

    public void SecondaryAttack()
    {
        Instantiate(fireball, firePoint.position, firePoint.rotation);
    }

    //Animation Event Functions
    public void return1()
    {
        if(noOfClicks >= 2)
        {
            playerAnimator.SetBool("Attack2", true);
            PlayerController.Instance.isAttacking = true;
        } else
        {
            PlayerController.Instance.isAttacking = false;
            playerAnimator.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }

    public void return2()
    {
        if (noOfClicks >= 3)
        {
            playerAnimator.SetBool("Attack3", true);
            PlayerController.Instance.isAttacking = true;
        }
        else
        {
            PlayerController.Instance.isAttacking = false;
            playerAnimator.SetBool("Attack2", false);
            noOfClicks = 0;
        }
    }

    public void return3()
    {
        PlayerController.Instance.isAttacking = false;
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

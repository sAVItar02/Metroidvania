using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyHealthBarBehaviour healthBar;

    [SerializeField] float moveSpeed = 1f;
    bool canMove = true;
    bool isAttacking = false;
    bool canSlimeAttack = true;
    [Space]
    [Header("Attack Params")]
    [SerializeField] float attackRange = 2f;
    [SerializeField] float rayCastRange = 0.5f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int damageToDeal = 20;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth = 30;
    [SerializeField] GameObject damageText;
    [Space]
    [Header("Collision Params")]
    [SerializeField] Transform raycastPoint;
    [SerializeField] float detectionRange = 0.5f;
    [SerializeField] LayerMask groundLayer;

    private float attackTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthBar = GetComponentInChildren<EnemyHealthBarBehaviour>();
        attackTimer = timeBetweenAttacks;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        if (canMove && !isAttacking)
        {
            if (IsFacingRight())
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        } else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        //Attack Stuff
        CanSeePlayer();
        isCollidingWithWall();

        //Timer For Attack
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer <= 0)
        {
            canSlimeAttack = true;
        }
    }

    private void isCollidingWithWall()
    {
        RaycastHit2D hitForward = Physics2D.Raycast(raycastPoint.transform.position, IsFacingRight()?Vector2.right:Vector2.left, detectionRange, groundLayer);
        RaycastHit2D hitDown = Physics2D.Raycast(raycastPoint.transform.position, Vector2.down, detectionRange);

        if(hitForward.collider != null)
        {
            if(hitForward.collider.gameObject.CompareTag("Ground"))
            {
                transform.Rotate(0f, 180f, 0f);
            }
        }

        if (hitDown.collider == false)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }
    private void CanSeePlayer()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, rayCastRange, playerLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayCastRange, playerLayer);
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            if ((hitLeft.distance <= attackRange || hitRight.distance <= attackRange) && !PlayerController.Instance.isDead)
            {
                Attack();
            }
            else StopAttack();
        }
        else StopAttack();
    }

    private void Attack()
    {
        if(canSlimeAttack)
        {
            isAttacking = true;
            canMove = false;
            attackTimer = timeBetweenAttacks;
            canSlimeAttack = false;
            anim.SetBool("Attack", true);
        } else
        {
            canMove = false;
            anim.SetBool("Attack", false);
        }
    }

    public void StopAttack()
    {
        canMove = true;
        isAttacking = false;
        anim.SetBool("Attack", false);
    }

    public void DealDamage()
    {
        if(!PlayerController.Instance.isDead)
        {
            Collider2D[] playerHit = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

            foreach (Collider2D player in playerHit)
            {
                player.GetComponent<PlayerController>().TakeDamage(damageToDeal);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);
        anim.SetTrigger("Hurt");

        if(damageText)
        {
            ShowDamageText(damage);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    } 

    private void Die()
    {
        canMove = false;
        anim.SetBool("isDead", true);
        rb.bodyType = RigidbodyType2D.Kinematic; //Make rigid body static to stop enemy movement 
        GetComponent<Collider2D>().enabled = false;
    }

    void ShowDamageText(int damage)
    {
        var text = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        var textChild = text.GetComponentInChildren<TextMesh>();
        textChild.text = damage.ToString();
        if (damage > 25)
        {
            Vector3 scale = new Vector3(0.06f, 0.06f, 0.06f);
            textChild.color = Color.red;
            textChild.transform.localScale = scale;
            CinemachineShake.Instance.ShakeCamera(5f, 0.2f);
        }
    }

    public void DestroySlime()
    {
        CinemachineShake.Instance.ShakeCamera(3f, 0.2f);
        Destroy(gameObject);
    } 
    //Helper Functions
    private bool IsFacingRight()
    {
        //return transform.localScale.x > Mathf.Epsilon;
        return transform.rotation == Quaternion.Euler(0f, 0f, 0f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * rayCastRange);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * rayCastRange);
        Gizmos.DrawLine(raycastPoint.transform.position, raycastPoint.transform.position + (IsFacingRight() ? Vector3.right : Vector3.left) * detectionRange);
        Gizmos.DrawLine(raycastPoint.transform.position, raycastPoint.transform.position + Vector3.down * detectionRange);
    }

    public void SetIsAttackingFalse()
    {
        isAttacking = false;
    }
}

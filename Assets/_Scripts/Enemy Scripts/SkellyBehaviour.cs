using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyBehaviour : MonoBehaviour
{
    #region Variables
    [Header("Attack Params")]
    [SerializeField] float attackDistance;
    [SerializeField] float followSpeed;
    [SerializeField] float timer;
    [Space]
    [Header("Patrol Params")]
    [SerializeField] Transform leftLimit;
    [SerializeField] Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    [Space]
    [Header("Health And Damage Params")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] GameObject damageText;
    [SerializeField] int damageToDeal = 10;
    [Space]
    [Header("Drops")]
    [SerializeField] GameObject _coin;
    [SerializeField] GameObject _healthPotion;
    [SerializeField] int healthPotionProb = 5;
    [SerializeField] GameObject _exp;
    [Space]
    [Header("Attack Params")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask playerLayer;

    private Animator anim;
    private new SpriteRenderer renderer;
    private Rigidbody2D rb;
    private EnemyHealthBarBehaviour enemyHealthBar;
    private float distance; //Dist b/w enemy and player
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    public bool isAttacking;
    private GameObject expTracker;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        enemyHealthBar = GetComponentInChildren<EnemyHealthBarBehaviour>();
        enemyHealthBar.SetHealth(currentHealth, maxHealth);
        intTimer = timer;
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        SelectTarget();
    }

    private void Start()
    {
        expTracker = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {

        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("SkellyAttack")) // Check if player is inside of limits
        {
            SelectTarget();
        }

        if(inRange)
        {
            EnemyLogic();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position); // Distance b/w enemy and player

       
        if(distance > attackDistance)
        {
            StopAttack();
        } else if(attackDistance > distance && !cooling && !PlayerController.Instance.isDead)
        {
            Attack();
        }

        if(cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        Vector2 targetPosition = new Vector2(target.position.x, transform.position.y); // Position to move to
        if(isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 0 * Time.deltaTime);
        } else
        {
            anim.SetBool("canWalk", true);
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("SkellyEnemy"))
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime); // Move Enemy to player position
            }
        }
    }

    void Attack()
    { 
        isAttacking = true;
        timer = intTimer; // Reset Timer when player enters attack range
        attackMode = true; // Check is enemy can attack

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TrigerCooling()
    {
        cooling = true;
    }

    bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        } else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        } else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    //Attack And Damage Methods
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        enemyHealthBar.SetHealth(currentHealth, maxHealth);
        anim.SetTrigger("Hurt");
        GetComponent<AudioSource>().Play();
        if (damageText)
        {
            ShowDamageText(damage);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void DealDamage()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(damageToDeal);
        }
    }

    void Die()
    {
        //Spawn random drop
        RandomDrop();

        //Handle Death
        anim.SetBool("isDead", true); // Start Death Anim
        CinemachineShake.Instance.ShakeCamera(5f, 0.2f); 
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; //Make rigid body static to stop enemy movement 
        GetComponent<Collider2D>().enabled = false; // Disable all colliders
        StartCoroutine("FadeOut"); // Fade enemy out of existence 
        transform.Find("HotZone").GetComponent<SkellyHotzoneCheck>().enabled = false; // Disable Hot zone
        transform.Find("TriggerArea").GetComponent<SkellyTriggerAreaCheck>().enabled = false; // Disable Trigger area
        this.enabled = false;
    }

    void RandomDrop()
    {
        int randomExpCount = Random.Range(3, 8);
        for (int i=0; i<randomExpCount; i++)
        {
            var exp = Instantiate(_exp, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), Quaternion.identity);
            exp.GetComponent<ExpBehaviour>().target = expTracker.transform;
        }

        int decideDrop = Random.Range(0, 101);
        if(decideDrop > (100- healthPotionProb))
        {
            Vector2 forceToAdd = new Vector2(Random.Range(-5f, 5f), Random.Range(1f, 8f));
            var healthPotion = Instantiate(_healthPotion, transform.position, Quaternion.identity);
            healthPotion.GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Impulse);
        } else
        {
            int randomCoins = Random.Range(2, 5);
            for (int i = 0; i < randomCoins; i++)
            {
                var coin = Instantiate(_coin, transform.position, Quaternion.identity);
                Vector2 forceToAdd = new Vector2(Random.Range(-5f, 5f), Random.Range(1f, 8f));
                coin.GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Impulse);
            }
        }
        
    }

    void ShowDamageText(int damage)
    {
        var text = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        var textChild = text.GetComponentInChildren<TextMesh>();
        textChild.text = damage.ToString();
        if(damage > 25)
        {
            Vector3 scale = new Vector3(0.06f, 0.06f, 0.06f);
            textChild.color = Color.red;
            textChild.transform.localScale = scale;
            CinemachineShake.Instance.ShakeCamera(5f, 0.2f);
        }
    }

    IEnumerator FadeOut()
    {
        for(float f = 1f; f >= -0.05f; f -= 0.01f)
        {
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }

    //Helper Functions
    public void SetIsAttacking()
    {
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

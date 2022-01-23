using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyBehaviour : MonoBehaviour
{
    #region Variables
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

    private Animator anim;
    private float distance; //Dist b/w enemy and player
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    #endregion

    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
        SelectTarget();
    }


    // Update is called once per frame
    void Update()
    {

        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("SkellyAttack"))
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
        } else if(attackDistance > distance && !cooling)
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
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("SkellyEnemy"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y); // Position to move to

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime); // Move Enemy to player position
        }
    }

    void Attack()
    {
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
}

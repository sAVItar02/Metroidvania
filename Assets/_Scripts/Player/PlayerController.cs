using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    #region Variables
    public static PlayerController Instance { get; private set; }
    protected float moveInput;
    protected Rigidbody2D playerRigidbody;
    protected Animator playerAnimator;
    protected PlayerCollision coll;
    protected PlayerHealth healthCanvas;

    [Header("Jump and Move")]
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float jumpForce = 20f;
    [SerializeField] protected float coyoteTime = 0.2f;
    [SerializeField] protected float jumpBufferTime = 0.2f;
    protected float coyoteTimeCounter;
    protected float jumpBufferCounter;

    /*[Space]
    [Header("Rolling")]
    [SerializeField] float rollSpeed = 10f;
    float rollTime;
    [SerializeField] float startRollTime;*/

    
    [Space]
    [Header("Health Params")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;


    [Space]
    [Header("Bools")]
    protected bool isRunning;
    protected bool isWallJumping;
    protected bool isWallSliding;
    protected bool wasOnGround;
    protected bool isFacingRight = true;
    public bool isDead = false;
    [HideInInspector] public bool isAttacking;

    [Space]
    [Header("Particles")]
    [SerializeField] ParticleSystem walkParticles;
    [SerializeField] ParticleSystem jumpParticles;
    [SerializeField] ParticleSystem landParticles;
    [SerializeField] ParticleSystem slideParticles;

    [Space]
    [Header("Interactables")]
    [SerializeField] protected float boxLength = 1f;
    [SerializeField] protected float boxWidth = 1f;
    #endregion


    private void Awake()
    {
        Instance = this;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        coll = GetComponent<PlayerCollision>();
        healthCanvas = GetComponent<PlayerHealth>();

        currentHealth = maxHealth;
        isDead = false;
    }

    void Update()
    {
        if(!isDead) 
        { 
            CheckInput();
            Run();
            HandleJump();
        }
        CheckFall();
        CheckJump();
        UpdateAnimations();

        if(moveInput < 0 && isFacingRight && !isAttacking)
        {
            FlipSprite();
        } else if( moveInput > 0 && !isFacingRight && !isAttacking)
        {
            FlipSprite();
        }

        // Play Land Particles
        if(!wasOnGround && coll.isOnGround)
        {
            playLandParticles();
        }

        wasOnGround = coll.isOnGround;

        //Interaction
        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();
    }

    protected void CheckInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    protected void HandleJump()
    {
        //Handle Coyote Time
        if (coll.isOnGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //Handle Jump Buffering
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //Handle Jump
        if (jumpBufferCounter >= 0f && coyoteTimeCounter > 0f && !isAttacking)
        {
            playerRigidbody.velocity += Vector2.up * jumpForce;
            playJumpParticles();
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
            playerAnimator.SetBool("isJumping", true);
        } else
        {
            playerAnimator.SetBool("isJumping", false);
        }
    }

    protected void Run()
    {
        if(isAttacking)
        {
            playerRigidbody.velocity = new Vector2(moveInput * 0f, playerRigidbody.velocity.y);
        } else
        {
            Vector2 playerVelocity = new Vector2(moveInput * speed, playerRigidbody.velocity.y);
            playerRigidbody.velocity = playerVelocity;

            isRunning = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
            playerAnimator.SetBool("isRunning", isRunning);
        }
        
    }

    

    public void TakeDamage(int damage)
    {
        if(!isAttacking)  // Doubtful
        {
            currentHealth -= damage;
            playerAnimator.SetTrigger("Hurt");
            healthCanvas.TakeDamage(damage);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        playerRigidbody.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        isDead = true;
        playerAnimator.SetBool("isDead", true);
        CinemachineShake.Instance.ShakeCamera(5f, 0.2f);
    }

    protected virtual void UpdateAnimations()
    {
        playerAnimator.SetBool("isRunning", isRunning);
    }

    protected void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //Handle Jump Anim
    protected void CheckJump()
    {
        if (!coll.isOnGround && playerRigidbody.velocity.y > 0)
        {
            playerAnimator.ResetTrigger("isOnGround");
            playerAnimator.SetBool("isJumping", true);
        }
        else
        {
            playerAnimator.SetTrigger("isOnGround");
            playerAnimator.SetBool("isJumping", false);
        }
    }

    //Handle Falling Animation
    protected void CheckFall()
    {
        if(!coll.isOnGround && playerRigidbody.velocity.y < 0 && !coll.isTouchingWall)
        {
            playerAnimator.SetBool("isFalling", true);
        } else
        {
            playerAnimator.SetBool("isFalling", false);
        }
    }

    //Helper Functions
    public bool CanAttack()
    {
        return (moveInput == 0 && coll.isOnGround && !isWallSliding && !isWallJumping);
    }
    public void playWalkParticles()
    {
        walkParticles.Play();
    }

    public void playLandParticles()
    {
        landParticles.Play();
    }
    public void playSlideParticles()
    {
        slideParticles.Play();
    }
    public void playJumpParticles()
    {
        jumpParticles.Play();
    }


    //---------INTERACTABLES-------------
    protected void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(boxWidth, boxLength), 0, Vector2.zero);

        if(hits.Length > 0)
        {
            foreach(RaycastHit2D rc in hits)
            {
                if(rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(boxWidth, boxLength));
    }

}

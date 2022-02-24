using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    #region Variables
    public static PlayerController Instance { get; private set; }
    private float moveInput;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private PlayerCollision coll;

    [Header("Jump and Move")]
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float jumpBufferTime = 0.2f;
    float coyoteTimeCounter;
    float jumpBufferCounter;

    [Space]
    [Header("Rolling")]
    [SerializeField] float rollSpeed = 10f;
    float rollTime;
    [SerializeField] float startRollTime;

    [Space]
    [Header("Wall Jumping and Sliding")]
    [SerializeField] float slideSpeed = -1f;
    [SerializeField] float xWallForce;
    [SerializeField] float yWallForce;
    [SerializeField] float wallJumpTime;
    [Space]
    [Header("Health Params")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;


    [Space]
    [Header("Bools")]
    bool isRunning;
    bool isWallJumping;
    bool isWallSliding;
    bool wasOnGround;
    bool isFacingRight = true;
    public bool isDead = false;
    [HideInInspector] public bool isAttacking;

    [Space]
    [Header("Particles")]
    [SerializeField] ParticleSystem walkParticles;
    [SerializeField] ParticleSystem jumpParticles;
    [SerializeField] ParticleSystem landParticles;
    [SerializeField] ParticleSystem slideParticles;
    #endregion


    private void Awake()
    {
        Instance = this;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        coll = GetComponent<PlayerCollision>();
        rollTime = startRollTime;

        currentHealth = maxHealth;
        isDead = false;
    }

    void Update()
    {
        CheckInput();
        Run();
        //FlipSprite();
        //HandleRoll();
        HandleJump();
        CheckFall();
        CheckJump();
        HandleWallSlide();
        UpdateAnimations();

        if(moveInput < 0 && isFacingRight)
        {
            FlipSprite();
        } else if( moveInput > 0 && !isFacingRight)
        {
            FlipSprite();
        }

        // Play Land Particles
        if(!wasOnGround && coll.isOnGround)
        {
            playLandParticles();
        }

        wasOnGround = coll.isOnGround;
    }

    void CheckInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

   /* void HandleRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isRunning)
        {
            bool isFacingRight = playerRigidbody.transform.localScale.x == 1 ? true : false;
            if (isFacingRight)
            {
                //playerRigidbody.AddForce(Vector2.right * rollSpeed, ForceMode2D.Impulse);
                playerAnimator.SetTrigger("Roll");
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x * 12 + rollSpeed, playerRigidbody.velocity.y);
            }
            else
            {
                playerRigidbody.velocity += Vector2.left * rollSpeed;
                playerAnimator.SetTrigger("Roll");
            }
        }
    }*/

    void HandleJump()
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
        if (jumpBufferCounter >= 0f && coyoteTimeCounter > 0f)
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

    void Run()
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

    void HandleWallSlide()
    {
        //Handle wall slide
        if (coll.isTouchingWall && !coll.isOnGround && playerRigidbody.velocity.y < 0 && moveInput != 0)
        {
            isWallSliding = true;
        }
        else { isWallSliding = false; }

        if (isWallSliding)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, slideSpeed);
            playerAnimator.SetTrigger("isWallSliding");
        }

        //Handle wall jump
        if (Input.GetButtonDown("Jump") && isWallSliding)
        {
            isWallJumping = true;
            Invoke("SetWallJumpingFalse", wallJumpTime);
        }

        if (isWallJumping)
        {
            playerRigidbody.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerAnimator.SetTrigger("Hurt");

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

    private void UpdateAnimations()
    {
        playerAnimator.SetBool("isWallSliding", isWallSliding);
        playerAnimator.SetBool("isRunning", isRunning);
    }

    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //Handle Jump Anim
    void CheckJump()
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
    void CheckFall()
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
    void SetWallJumpingFalse()
    {
        isWallJumping = false;
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private PlayerCollision coll;

    [Header("Stats")]
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float slideSpeed = 5f;

    [Space]
    [Header("Bools")]
    [SerializeField] bool isRunning;
    [SerializeField] bool isJumping;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        coll = GetComponent<PlayerCollision>();
    }

    void FixedUpdate()
    {
        Run();
        FlipSprite();
    }

    void Update()
    {
        CheckFall();
        CheckJump();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!coll.isOnGround) return;
        if(value.isPressed)
        {
            isJumping = true;
            playerRigidbody.velocity += Vector2.up * jumpForce;
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;

        isRunning = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", isRunning);
    }

    void FlipSprite()
    {
        bool playeHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        if(playeHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    void CheckJump()
    {
        if (isJumping && !coll.isOnGround && playerRigidbody.velocity.y > 0)
        {
            playerAnimator.SetBool("isJumping", true);
            isJumping = false;
        } else
        {
            playerAnimator.SetBool("isJumping", false);
        }
    }

    void CheckFall()
    {
        if(!coll.isOnGround && playerRigidbody.velocity.y < 0)
        {
            playerAnimator.SetBool("isFalling", true);
        } else
        {
            playerAnimator.SetBool("isFalling", false);
        }
    }
}

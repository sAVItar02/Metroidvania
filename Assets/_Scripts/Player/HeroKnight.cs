using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroKnight : PlayerController
{
    [Space]
    [Header("Wall Jumping and Sliding")]
    [SerializeField] float slideSpeed = -1f;
    [SerializeField] float xWallForce;
    [SerializeField] float yWallForce;
    [SerializeField] float wallJumpTime;

    // Update is called once per frame
    void Update()
    {
        if(!base.isDead)
        {
            CheckInput();
            Run();
            HandleJump();
        }

        CheckFall();
        CheckJump();
        HandleWallSlide();
        UpdateAnimations();

        if (moveInput < 0 && isFacingRight)
        {
            FlipSprite();
        }
        else if (moveInput > 0 && !isFacingRight)
        {
            FlipSprite();
        }

        // Play Land Particles
        if (!wasOnGround && coll.isOnGround)
        {
            playLandParticles();
        }

        wasOnGround = coll.isOnGround;

        //Interaction
        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();
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

    protected override void UpdateAnimations()
    {
        base.UpdateAnimations();
        playerAnimator.SetBool("isWallSliding", isWallSliding);
    }

    protected void SetWallJumpingFalse()
    {
        isWallJumping = false;
    }
}

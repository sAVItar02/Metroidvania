using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMulitplier = 2f;

    Rigidbody2D playerRigidbody;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (playerRigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMulitplier - 1) * Time.deltaTime;
        }
    }
}

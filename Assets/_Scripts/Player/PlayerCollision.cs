using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]
    [Header("Bools")]
    public bool isOnGround;
    public bool isTouchingWall;

    [Space]
    [Header("Collision")]
    [SerializeField] float collisionRadius = 0.25f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float wallCollisionRadius = 0.1f;
    [SerializeField] Transform wallCheck;

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.transform.position, collisionRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.transform.position, wallCollisionRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, collisionRadius);
        Gizmos.DrawWireSphere(wallCheck.transform.position, wallCollisionRadius);
    }
}

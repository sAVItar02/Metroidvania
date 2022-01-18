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

    [Space]
    [Header("Collision")]
    [SerializeField] float collisionRadius = 0.25f;
    [SerializeField] Transform groundCheck;

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.transform.position, collisionRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, collisionRadius);
    }
}

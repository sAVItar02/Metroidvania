using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumCollider : MonoBehaviour
{
    [SerializeField] int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyTriggerAreaCheck : MonoBehaviour
{
    private SkellyBehaviour enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<SkellyBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collision.transform;
            enemyParent.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}

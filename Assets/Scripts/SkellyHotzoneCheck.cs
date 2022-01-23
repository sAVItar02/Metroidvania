using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyHotzoneCheck : MonoBehaviour
{
    private SkellyBehaviour enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        enemyParent = GetComponentInParent<SkellyBehaviour>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange )
        {
            enemyParent.Flip();
            Debug.Log("Flipped");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag + collision.gameObject.name + collision.gameObject);
        if(collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag + collision.gameObject.name + collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exit");
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }
}

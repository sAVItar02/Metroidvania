using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : MonoBehaviour
{
    BoxCollider2D[] boxCollider;
    private void Start()
    {
        boxCollider =  gameObject.GetComponentsInChildren<BoxCollider2D>();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Animator>().enabled = true;
        }
    }

    public void enableCollider()
    {
        boxCollider[1].offset = new Vector2(0, -0.25f);
        boxCollider[1].size = new Vector2(1.3f, 5.4f);
    }

    public void disableCollider()
    {
        boxCollider[1].offset = new Vector2(0.375f, -2.62f);
        boxCollider[1].size = new Vector2(1.3f, 0.68f);
    }
}

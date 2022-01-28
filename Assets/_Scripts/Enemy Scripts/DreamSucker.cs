using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamSucker : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (transform.position.x < PlayerController.Instance.transform.position.x)
        {
            sprite.flipX = false;
        } else
        {
            sprite.flipX = true;
        }
    }
}

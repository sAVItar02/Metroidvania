using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamSucker : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    Vector2 targetPosition;
    [SerializeField] float speed;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        targetPosition = GetRandomPosition();
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

        if ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            targetPosition = GetRandomPosition();
        }
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }
}

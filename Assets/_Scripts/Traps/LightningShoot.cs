using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningShoot : MonoBehaviour
{
    [SerializeField] int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }

    

    /*public void shootBolt()
    {
        Collider2D hitPLayer = Physics2D.OverlapBox(new Vector2(shootPoint.transform.position.x, shootPoint.transform.position.y), new Vector2(shootPointWidth, shootPointLength), 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(shootPoint.position, new Vector3(shootPointWidth, shootPointLength, 0));
    }*/
}

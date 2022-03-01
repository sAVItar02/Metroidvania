using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Openable : Interactable
{
    private Animator anim;
    private bool isOpen = false;
    private bool hasBeenOpened = false;
    [SerializeField] bool isGolden = false;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject redGem;
    [SerializeField] GameObject blueGem;

    private void Start()
    {
        anim = GetComponent<Animator>();
        interactIcon.SetActive(false);
    }
    public override void Interact()
    {
        if(isOpen)
        {
            anim.SetBool("isOpen", true);
        } else
        {
            anim.SetBool("isOpen", false);
        }

        isOpen = !isOpen;
    }

    public void DropRandomItem()
    {
        Vector2 forceToAdd = new Vector2(Random.Range(-5f, 5f), Random.Range(1f, 8f));
        if (!hasBeenOpened)
        {
            if(isGolden)
            {
                Instantiate(blueGem, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
                blueGem.GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Impulse);
            } else
            {
                for(int i=0; i<=20; i++)
                {
                    Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
                    coin.GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Impulse);
                }
            }
        }
    } 

}

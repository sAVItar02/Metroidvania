using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Openable : Interactable
{
    private Animator anim;
    private bool canBeOpened = true;
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
        if(canBeOpened)
        {
            anim.SetBool("isOpen", true);
        } else
        {
            anim.SetBool("isOpen", false);
        }

        canBeOpened = !canBeOpened;
    }

    public void DropRandomItem()
    {
        
        if (!hasBeenOpened)
        {
            if(isGolden)
            {
                var _blueGem = Instantiate(blueGem, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
                Vector2 forceToAdd = new Vector2(Random.Range(-5f, 5f), Random.Range(1f, 8f));
                _blueGem.GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Impulse);
            } else
            {
                for(int i=0; i<=20; i++)
                {
                    var _coin = Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
                    Vector2 forceToAdd = new Vector2(Random.Range(-5f, 5f), Random.Range(1f, 8f));
                    _coin.GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Impulse);
                }
            }

            hasBeenOpened = true;
        }
    } 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    #region Variables
    public enum typeOfPickup { coin, healthPotion, blueGem }
    //public int healthPotionProb = 5;

    [SerializeField] typeOfPickup pickup;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /*if (pickup == typeOfPickup.coin)
            {
                Debug.Log("Coin picked");
                Destroy(gameObject);
            } else if (pickup == typeOfPickup.healthPotion)
            {
                Debug.Log("Potion picked");
                Destroy(gameObject);
            }*/

            switch (pickup) {
                case typeOfPickup.coin:
                    Debug.Log("Coin picked");
                    Destroy(gameObject);
                    break;
                case typeOfPickup.healthPotion:
                    Debug.Log("Potion picked");
                    Destroy(gameObject);
                    break;
                case typeOfPickup.blueGem:
                    Debug.Log("Blue Gem Picked");
                    Destroy(gameObject);
                    break;
            }

        }
    }
}

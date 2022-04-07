using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PickupHandler : MonoBehaviour
{
    #region Variables
    public enum typeOfPickup { coin, healthPotion, blueGem }
    //public int healthPotionProb = 5;

    [SerializeField] typeOfPickup pickup;
    [SerializeField] GameSession gameSession;

    private GameController game;
    private MainCanvas mainCanvas;
    private void Start()
    {
        game = FindObjectOfType<GameController>();
        mainCanvas = FindObjectOfType<MainCanvas>();
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            switch (pickup) {
                case typeOfPickup.coin:
                    Debug.Log("Coin picked");
                    game.totalCoins += 20;
                    gameSession.coinsInCurrentLevel += 20;
                    mainCanvas.coinsText.text = gameSession.coinsInCurrentLevel.ToString();
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

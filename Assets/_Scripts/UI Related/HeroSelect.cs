using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeroSelect : MonoBehaviour
{
    [SerializeField] GameObject characterInfo;
    [SerializeField] GameObject characterSelect;
    [SerializeField] TextMeshProUGUI totalCoins;
    [SerializeField] AudioManager audioManager;

    void Start()
    {
        GameController game = FindObjectOfType<GameController>();
        totalCoins.text = game.totalCoins.ToString();
    }


    public void ClickHero(int index)
    {
        audioManager.PlayClickSound();
        switch (index)
        {
            case 0:
                OpenCharacterInfo(index);
                break;
            case 1:
                OpenCharacterInfo(index);
                break;
            case 2:
                OpenCharacterInfo(index);
                break;
            case 3:
                OpenCharacterInfo(index);
                break;
            default:
                OpenCharacterInfo(index);
                break;
        }
    }

    void OpenCharacterInfo(int index)
    {
        characterInfo.SetActive(true);
        characterSelect.SetActive(false);
        characterInfo.GetComponent<HeroDisplay>().SetIndex(index);
        characterInfo.GetComponent<HeroDisplay>().SetDetails(index);
    }

    public void playHoverSound(Image characterImg)
    {
        audioManager.PlayHoverSound();
        characterImg.color = new Color(255, 255, 255);
    }

    public void PointerExit(Image characterImg)
    {
        characterImg.color = new Color(0, 0, 0);
    }
/*    public void Print(string msg)
    {
        Debug.Log(msg);
    }*/
}

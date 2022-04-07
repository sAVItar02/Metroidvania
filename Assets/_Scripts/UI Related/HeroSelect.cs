using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroSelect : MonoBehaviour
{
    [SerializeField] GameObject characterInfo;
    [SerializeField] GameObject characterSelect;
    [SerializeField] TextMeshProUGUI totalCoins;

    void Start()
    {
        GameController game = FindObjectOfType<GameController>();
        totalCoins.text = game.totalCoins.ToString();
    }


    public void ClickHero(int index)
    {
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

/*    public void Print(string msg)
    {
        Debug.Log(msg);
    }*/
}

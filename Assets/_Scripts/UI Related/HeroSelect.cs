using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelect : MonoBehaviour
{
    [SerializeField] GameObject characterInfo;
    [SerializeField] GameObject characterSelect;

    public void ClickHero(int index)
    {
        Debug.Log(index);
        switch (index)
        {
            case 0:
                OpenCharacterInfo(index);
                Debug.Log("Hero Knight Clicked");
                break;
            case 1:
                OpenCharacterInfo(index);
                Debug.Log("Fire Knight Clicked");
                break;
            case 2:
                OpenCharacterInfo(index);
                Debug.Log("Rogue Knight Clicked");
                break;
            case 3:
                OpenCharacterInfo(index);
                Debug.Log("Fantasy Knight Clicked");
                break;
            default:
                OpenCharacterInfo(index);
                Debug.Log("Default fired");
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

    public void Print(string msg)
    {
        Debug.Log(msg);
    }
}

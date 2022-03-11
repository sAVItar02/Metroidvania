using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelect : MonoBehaviour
{
    [SerializeField] GameObject characterInfo;
    [SerializeField] GameObject characterSelect;

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
                Debug.Log("Default fired");
                break;
        }
    }

    void OpenCharacterInfo(int index)
    {
        Debug.Log(index);
        characterInfo.SetActive(true);
        characterSelect.SetActive(false);
        characterInfo.GetComponent<HeroDisplay>().SetDetails(index);
    }
}

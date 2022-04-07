using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainCanvas : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    void Start()
    {
        coinsText.text = 0.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroDisplay : MonoBehaviour
{
    [SerializeField] HeroInfo []info;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descText;

    [SerializeField] Image artworkImage;
    [SerializeField] RectTransform rectTransform;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] Animator animator;

    [SerializeField] Image leftArrow;
    [SerializeField] Image rightArrow;
    [HideInInspector] public int currentIndex = 0;
    /*void Start()
    {
        currentIndex = 0;
        SetDetails(currentIndex);
    }*/

    public void RightShift()
    {
        if (currentIndex <= info.Length-1)
        {
            if (currentIndex == info.Length - 1) currentIndex = 0;
            else currentIndex++;
            SetDetails(currentIndex);
        }
    }

    public void LeftShift()
    {
        if (currentIndex >= 0)
        {
            if (currentIndex == 0) currentIndex = info.Length - 1;
            else currentIndex--;
            SetDetails(currentIndex);
        }
    }

    public void SetDetails(int index) //sets all the details on the cards
    {
        nameText.text = info[index].HeroName;
        descText.text = info[index].description;

        artworkImage.sprite = info[index].artwork;
        rectTransform.sizeDelta = new Vector2(info[index].imgWidth, info[index].imgHeight);
        rectTransform.anchoredPosition = new Vector3(0, info[index].imgPositionY, 0);

        healthText.text = info[index].health.ToString();
        attackText.text = info[index].attackValue.ToString();
        levelText.text = "Level " + info[index].level.ToString();
        animator.runtimeAnimatorController = info[index].animationController;
    }
}

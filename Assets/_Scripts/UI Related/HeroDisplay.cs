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

    private int currentIndex = 0;
    void Start()
    {
        currentIndex = 0;
        leftArrow.enabled = false;
        SetDetails(currentIndex);
    }

    public void RightShift()
    {
        currentIndex++;
        leftArrow.enabled = true;
        if (currentIndex <= info.Length-1)
        {
            SetDetails(currentIndex);
            if(currentIndex == info.Length-1)
            {
                rightArrow.enabled = false;
            }
        } else if(currentIndex == info.Length-1 || currentIndex > info.Length-1)
        {
            rightArrow.enabled = false;
        }
    }

    public void LeftShift()
    {
        currentIndex--;
        rightArrow.enabled = true;       
        if (currentIndex >= 0)
        {
            SetDetails(currentIndex);
            if (currentIndex == 0)
            {
                leftArrow.enabled = false;
            }
        }
        else if (currentIndex <= 0)
        {
            leftArrow.enabled = false;
        }
    }

    void SetDetails(int index) //sets all the details on the cards
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

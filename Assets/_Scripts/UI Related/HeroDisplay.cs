using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HeroDisplay : MonoBehaviour
{
    [SerializeField] HeroInfo []info;
    [SerializeField] GameSession gameSession;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descText;

    [SerializeField] Image artworkImage;
    [SerializeField] RectTransform rectTransform;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI priceText;

    [SerializeField] Animator animator;

    [SerializeField] GameObject selectButton;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject priceField;

    public int currentIndex = 0;
 

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

        if(info[index].priceInCoins != 0)
        {
            priceField.SetActive(true);
            priceText.text = info[index].priceInCoins.ToString();
        }

        if(info[index].priceInCoins == 0)
        {
            priceField.SetActive(false);
        }

        if (info[index].isUnlocked)
        {
            selectButton.SetActive(true);
            selectButton.GetComponent<Button>().onClick.AddListener(customListener);
            buyButton.SetActive(false);
        }
        else { 
            buyButton.SetActive(true);
            selectButton.SetActive(false);
        }
    }

    void customListener()
    {
        gameSession.selectedHero = currentIndex;
    }

    public void SetIndex(int givenIndex) //sets the currentIndex value to the given value
    {
        currentIndex = givenIndex;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

}

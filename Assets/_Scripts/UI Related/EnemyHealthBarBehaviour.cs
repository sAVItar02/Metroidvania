using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBarBehaviour : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Color low;
    [SerializeField] Color high;
    [SerializeField] Vector3 offset;

    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        if(health < 0)
        {
            slider.gameObject.SetActive(false);
        }
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
}

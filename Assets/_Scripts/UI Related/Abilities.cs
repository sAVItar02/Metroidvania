using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    private ComboAttack comboAttak;

    [Header("Ability 1")]
    [SerializeField] Image abilityImage1;
    bool isCooldown = false;
    void Start()
    {
        abilityImage1.fillAmount = 0;
        comboAttak = GetComponentInParent<ComboAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
    }

    void Ability1()
    {
        if(Input.GetMouseButtonDown(1) && comboAttak.canUseSecondary && !isCooldown)
        {
            isCooldown = true;
            abilityImage1.fillAmount = 1;
        }

        if(isCooldown)
        {
            abilityImage1.fillAmount -= 1 / comboAttak.secondaryAttkTimeDelay * Time.deltaTime;

            if(abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI HealthText; 
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        SetHealth(health);

       
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        HealthText.text = string.Format("{0} / {1}", health, slider.maxValue);
    }
}

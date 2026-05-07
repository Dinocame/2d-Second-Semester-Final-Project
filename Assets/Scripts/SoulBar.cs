using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulBar : MonoBehaviour
{
    public Slider slider;

    public void SetSoulPowerMax(float soulPowerMax)
    {
        slider.maxValue = soulPowerMax;
    }
    
    public void SetSoulPower(float soulPower)
    {
        slider.value = soulPower;
    }
}

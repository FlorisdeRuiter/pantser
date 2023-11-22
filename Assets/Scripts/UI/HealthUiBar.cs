using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUiBar : UiBar
{
    public void UpdateHealthBar(float currentAmount, float maxAmount)
    {
        _bar.value = currentAmount / maxAmount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpUiBar : UiBar
{
    public void UpdateExperienceBar(float currentAmount, float maxAmount)
    {
        _bar.value = currentAmount / maxAmount;
    }
}

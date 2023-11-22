using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUiText : UiText
{
    public void UpdateTimeUi(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = timeToDisplay % 60;
        _textComponent.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

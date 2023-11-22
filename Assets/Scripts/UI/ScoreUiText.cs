using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUiText : UiText
{
    public void UpdateScoreUi(float value)
    {
        _textComponent.text = value.ToString();
    }
}

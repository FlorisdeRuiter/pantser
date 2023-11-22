using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiText : MonoBehaviour
{
    protected TextMeshProUGUI _textComponent;
    protected GameManager _gameManager;

    private void Start()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
        _gameManager = GameManager.GetInstance();
    }

    public void UpdateText(string displayText)
    {
        _textComponent.text = displayText;
    }
}

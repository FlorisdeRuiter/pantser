using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiText : MonoBehaviour
{
    protected TextMeshProUGUI _textComponent;

    private void Start()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }
}

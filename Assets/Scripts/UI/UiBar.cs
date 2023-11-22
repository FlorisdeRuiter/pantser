using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    protected Slider _bar;

    private void Start()
    {
        _bar = GetComponent<Slider>();
    }
}

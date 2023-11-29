using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddGameManagerEventToButton : MonoBehaviour
{
    private void Start()
    {
        AddGameManagerEvent();
    }

    private void AddGameManagerEvent()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.GetInstance().TogglePause);
    }
}

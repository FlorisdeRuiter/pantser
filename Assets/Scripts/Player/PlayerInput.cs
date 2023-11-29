using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    [SerializeField] private SceneLoadData _pauseMenuData;

    private void Start()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (!_pauseMenuData.SceneReference.IsAssigned)
        {
            Time.timeScale = 1;
            _sceneLoader.SceneUnload(_pauseMenuData);
        }
        else
        {
            Time.timeScale = 0;
            _sceneLoader.SceneLoad(_pauseMenuData);
        }
    }
}

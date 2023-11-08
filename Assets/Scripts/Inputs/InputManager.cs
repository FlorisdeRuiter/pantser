using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum MovementInputType
    {
        Keyboard,
        Controller,
        Mouse
    }

    [Header("Input Type")]
    public MovementInputType inputType;

    private static InputManager _instance;
    public static InputManager GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<InputManager>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<InputManager>();
        }

        return _instance;
    }
}

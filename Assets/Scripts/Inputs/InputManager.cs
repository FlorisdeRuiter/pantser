using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Tracks the type of input player is using
/// </summary>
public class InputManager : MonoBehaviour
{
    // !!!Not in use anymore!!!

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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public ScoreUiText ScoreUiText;
    public TimeUiText TimeUiText;
    public HealthUiBar HealthUiBar;
    public ExpUiBar ExpUiBar;
    private static UiManager _instance;

    public static UiManager GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<UiManager>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<UiManager>();
        }

        return _instance;
    }
}

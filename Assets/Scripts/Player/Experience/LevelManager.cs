using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    private CardManager _cardManager;
    private List<Card> _cardsOnUI;
    private int _currentLevel;
    private int _cardsNeededOnLevelUp;

    private void Start()
    {
        _cardManager = CardManager.GetInstance();
    }

    /// <summary>
    /// Updates the player level and displays cards
    /// </summary>
    public void LevelUp()
    {
        _currentLevel += 1;

        _cardManager.DisplayCards();
    }

    #region Get Instance
    public static LevelManager GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<LevelManager>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<LevelManager>();
        }

        return _instance;
    }
    #endregion
}

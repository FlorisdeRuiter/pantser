using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;

    private static LevelManager _instance;

    private int cardsNeededOnLevelUp;

    private CardManager cardManager;

    private List<Card> cardsOnUI;

    private void Start()
    {
        cardManager = CardManager.GetInstance();
    }

    public void LevelUp()
    {
        currentLevel += 1;

        cardManager.DisplayCards();
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

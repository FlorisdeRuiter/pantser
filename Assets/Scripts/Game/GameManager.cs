using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<ObjectPool> EnemyPoolList;
    private EnemySpawner _spawner;
    private UiManager _uiManager;

    private static GameManager _instance;

    private float _score;
    private float _gameTime;

    private void Start()
    {
        _spawner = EnemySpawner.GetInstance();
        EnemyPoolList = _spawner.EnemyPoolsList;
        _uiManager = UiManager.GetInstance();
    }

    private void Update()
    {
        IncreaseTimer();
    }

    public void IncreaseTimer()
    {
        _gameTime += Time.deltaTime;
        _uiManager.TimeUiText.UpdateTimeUi(_gameTime);
    }

    public void IncreaseScore(float value)
    {
        _score += value;
        _uiManager.ScoreUiText.UpdateScoreUi(_score);
    }

    public static GameManager GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<GameManager>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<GameManager>();
        }

        return _instance;
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private UiManager _uiManager;

    private static GameManager _instance;

    private void Start()
    {
        _uiManager = UiManager.GetInstance();
    }

    private void Update()
    {
        IncreaseTimer();

        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    private float _score = 0;
    public void IncreaseScore(float value)
    {
        _score += value;
        _uiManager.ScoreUiText.UpdateScoreUi(_score);
    }

    [SerializeField] private float _gameTime;
    [SerializeField] private float _totalGameTime;
    public void IncreaseTimer()
    {
        _gameTime += Time.deltaTime;
        _uiManager.TimeUiText.UpdateTimeUi(_gameTime);

        if (_gameTime >= _totalGameTime && !_gameHasEnded)
        {
            EndGame(true);
        }
    }

    [SerializeField] private UnityEvent _gameWinEvent;
    [SerializeField] private UnityEvent _gameLossEvent;
    [SerializeField] private bool _gameHasEnded;
    public void EndGame(bool hasWon)
    {
        if (hasWon)
            _gameWinEvent?.Invoke();
        else
            _gameLossEvent?.Invoke();

        _gameHasEnded = true;
        Time.timeScale = 0;
    }

    [SerializeField] private UnityEvent _pauseEvent;
    [SerializeField] private UnityEvent _unpauseEvent;
    [SerializeField] private bool _gameIsPaused;
    public void TogglePause()
    {
        if (_gameIsPaused)
        {
            Time.timeScale = 1;
            _unpauseEvent?.Invoke();
        }
        else
        {
            Time.timeScale = 0;
            _pauseEvent?.Invoke();
        }

        _gameIsPaused = !_gameIsPaused;
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

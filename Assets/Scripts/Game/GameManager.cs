using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PlayerManager Player;
    public UiManager UiManager;
    public CardManager CardManager;
    public AbilityManager AbilityManager;
    public ExperienceManager ExperienceManager;

    private static GameManager _instance;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerManager>();
        UiManager = FindObjectOfType<UiManager>();
        CardManager = FindObjectOfType<CardManager>();
        AbilityManager = FindObjectOfType<AbilityManager>();
        ExperienceManager = FindObjectOfType<ExperienceManager>();
    }

    private void Update()
    {
        IncreaseTimer();

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    private float _score = 0;
    public void IncreaseScore(float value)
    {
        _score += value;
        UiManager.ScoreUiText.UpdateScoreUi(_score);
    }

    [SerializeField] public float GameTime;
    [SerializeField] private float _totalGameTime;
    public void IncreaseTimer()
    {
        GameTime += Time.deltaTime;
        UiManager.TimeUiText.UpdateTimeUi(GameTime);

        if (GameTime >= _totalGameTime && !_gameHasEnded)
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

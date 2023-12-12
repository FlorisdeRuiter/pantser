using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ExperienceManager : MonoBehaviour
{
    [Header("Experience Data")]
    [SerializeField] private float _currentExp;
    [SerializeField] private float _requiredExpForLevelUp;
    [SerializeField] private float _requiredExpModifier;

    [Header("Player Level Data")]
    [SerializeField] private int _currentLevel;

    [Header("Event")]
    [SerializeField] private UnityEvent _levelUpEvent;

    private UiManager _uiManager;

    private void Start()
    {
        _uiManager = GameManager.GetInstance().UiManager;
        _uiManager.ExpUiBar.UpdateExperienceBar(_currentExp, _requiredExpForLevelUp);
    }

    public void OnGainExp(float expGained)
    {
        // Adds gained experience
        _currentExp += expGained;

        // Checks if gained experience is enough for level up
        if (_currentExp >= _requiredExpForLevelUp)
        {
            // Sets the excess exp
            float excessExp = _currentExp - _requiredExpForLevelUp;

            // Stores excess experience
            _currentExp = excessExp;

            _currentLevel += 1;

            _levelUpEvent?.Invoke();
        }

        _uiManager.ExpUiBar.UpdateExperienceBar(_currentExp, _requiredExpForLevelUp);

        GameManager.GetInstance().IncreaseScore(5);
    }

    public void IncreaseRequiredExp()
    {
        _requiredExpForLevelUp *= _requiredExpModifier;
    }
}

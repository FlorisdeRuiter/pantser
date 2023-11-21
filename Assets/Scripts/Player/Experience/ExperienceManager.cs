using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ExperienceManager : MonoBehaviour
{
    [Header("Experience Data")]
    [SerializeField] private float currentExp;
    [SerializeField] private float expNeededForLevelUp;

    [Header("Event")]
    [SerializeField] private UnityEvent levelUpEvent;

    private static ExperienceManager _instance;

    public void OnGainExp(float expGained)
    {
        // Adds gained experience
        currentExp += expGained;

        // Checks if gained experience is enough for level up
        if (currentExp >= expNeededForLevelUp)
        {
            // Sets the excess exp
            float excessExp = currentExp - expNeededForLevelUp;

            // Stores excess experience
            currentExp = excessExp;

            levelUpEvent?.Invoke();
        }
    }

    #region Get Instance
    public static ExperienceManager GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<ExperienceManager>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("ExperienceManager").AddComponent<ExperienceManager>();
        }

        return _instance;
    }
    #endregion
}
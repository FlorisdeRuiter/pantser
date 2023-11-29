using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;

    [Header("Ability")]
    public float ConstantDamageModifier = 0;
    public float ConstantIntervalModifier = 0;
    public float ConstantCooldownModifier = 0;

    [Header("Event")]
    public UnityEvent DamageEvent;

    private UiManager _uiManager;

    private static Player _instance;

    private void Start()
    {
        _health = _maxHealth;
        _uiManager = UiManager.GetInstance();
        _uiManager.HealthUiBar.UpdateHealthBar(_health, _maxHealth);
    }

    public void DoDamage(float damage)
    {
        _health -= damage;
        DamageEvent?.Invoke();
        _uiManager.HealthUiBar.UpdateHealthBar(_health, _maxHealth);

        if (_health <= 0)
        {
            GameManager.GetInstance().EndGame(false);
        }
    }

    #region Returns Tag
    public string GetPlayerTag()
    {
        return gameObject.tag;
    }
    #endregion

    #region Returns Transform
    public Transform GetTransform()
    {
        return transform;
    }
    #endregion 

    public static Player GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<Player>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<Player>();
        }

        return _instance;
    }
}

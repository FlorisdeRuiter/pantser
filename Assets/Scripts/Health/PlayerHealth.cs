using MyBox;
using UnityEngine;

public class PlayerHealth : Health, IHealable
{
    private UiManager _uiManager;

    protected override void Start()
    {
        base.Start();
        _uiManager = GameManager.GetInstance().UiManager;
    }

    protected float m_currentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            _uiManager.HealthUiBar.UpdateHealthBar(_currentHealth, MaxHealth);
        }
    }

    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;

        if (_currentHealth > MaxHealth)
        {
            _currentHealth = MaxHealth;
        }
    }

    protected override void Death()
    {
        GameManager.GetInstance().EndGame(false);
    }
}

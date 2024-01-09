
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
            _uiManager.HealthUiBar.UpdateHealthBar(_currentHealth, _maxHealth);
        }
    }

    public override void DoDamage(float damage)
    {
        m_currentHealth -= damage;

        if (_currentHealth <= 0)
            Death();

        OnDamaged?.Invoke();
    }

    public void Heal(float healAmount)
    {
        m_currentHealth += healAmount;

        if (_currentHealth > _maxHealth)
        {
            m_currentHealth = _maxHealth;
        }
    }

    protected override void Death()
    {
        GameManager.GetInstance().EndGame(false);
    }
}

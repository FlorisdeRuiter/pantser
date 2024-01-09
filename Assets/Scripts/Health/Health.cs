using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;
    [SerializeField] protected UnityEvent OnDamaged;
    [SerializeField] protected UnityEvent OnDeath;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void DoDamage(float damage)
    {
        _currentHealth -= damage;
        OnDamaged?.Invoke();

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    protected abstract void Death();
}

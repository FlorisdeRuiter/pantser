using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected float MaxHealth;
    [SerializeField] protected float _currentHealth;
    public float CurrentHealth => _currentHealth;

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }

    public void DoDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Death();
    }

    protected abstract void Death();
}

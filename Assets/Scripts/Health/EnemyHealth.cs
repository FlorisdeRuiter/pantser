public class EnemyHealth : Health
{
    private BaseEnemy _enemy;

    protected override void Start()
    {
        _enemy = GetComponent<BaseEnemy>();
        base.Start();
    }

    private void OnEnable()
    {
        _currentHealth = MaxHealth;
    }

    protected override void Death()
    {
        OnDeath.Invoke();
        _enemy.ReturnToPool();
        _enemy.DropExp();
        GameManager.GetInstance().IncreaseScore(_enemy.StatData.Value);
        EnemySpawner.GetInstance().CurrentEnemyValue -= _enemy.StatData.Value;
    }
    
    public void SetMaxHp(float maxHp)
    {
        MaxHealth = maxHp;
    }
}

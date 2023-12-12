public class EnemyHealth : Health
{
    private BaseEnemy _enemy;

    protected override void Start()
    {
        _enemy = GetComponent<BaseEnemy>();
        base.Start();
    }

    protected override void Death()
    {
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

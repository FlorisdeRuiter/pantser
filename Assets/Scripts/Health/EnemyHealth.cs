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
        _currentHealth = _maxHealth;
    }

    protected override void Death()
    {
        _enemy.DropExp();
        GameManager.GetInstance().IncreaseScore(_enemy.StatData.Value);
        _enemy.ReturnToPool();
    }

    public void SetMaxHp(float maxHp)
    {
        _maxHealth = maxHp;
    }
}

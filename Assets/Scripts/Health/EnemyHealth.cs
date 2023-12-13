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
        _enemy.DropExp();
        GameManager.GetInstance().IncreaseScore(_enemy.StatData.Value);
        Destroy(gameObject);
    }

    public void SetMaxHp(float maxHp)
    {
        MaxHealth = maxHp;
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StinkBugAura : AMeleeAttack
{
    [SerializeField] private List<BaseEnemy> _enemyList;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEnemy enemy;
        if (collision.TryGetComponent<BaseEnemy>(out enemy))
        {
            _enemyList.Add(enemy);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_timeUntilAttack > 0)
            return;

        foreach (BaseEnemy enemy in _enemyList.ToList())
        {
            enemy.Health.DoDamage(BaseDamage);
        }

        Attack();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BaseEnemy enemy;
        if (collision.TryGetComponent<BaseEnemy>(out enemy))
        {
            _enemyList.Remove(enemy);
        }
    }
}

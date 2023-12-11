using UnityEngine;

public class AMeleeAttack : AAttack
{
    protected virtual void Update()
    {
        _timeUntilAttack -= Time.deltaTime;
        transform.position = _player.transform.position;
    }

    protected override void Attack()
    {
        _anim.SetTrigger(_attackTrigger);

        _attackEvent.Invoke();
        _timeUntilAttack = _baseAttackInterval;
    }

    #region Collider toggle
    private void ToggleCollider()
    {
        Collider2D collider = GetComponentInChildren<Collider2D>();
        collider.enabled = !collider.enabled;
    }
    #endregion
}

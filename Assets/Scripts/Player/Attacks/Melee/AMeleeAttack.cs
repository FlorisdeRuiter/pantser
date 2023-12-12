using UnityEngine;

public class AMeleeAttack : AAttack
{
    protected virtual void Update()
    {
        transform.position = _player.transform.position;
        _timeUntilAttack -= Time.deltaTime;
    }

    protected override void Attack()
    {
        _anim.SetTrigger(_attackTrigger);

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

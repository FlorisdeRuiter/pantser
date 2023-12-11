using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaOfEffect : MonoBehaviour
{
    [SerializeField] private float _despawnTime;
    [SerializeField] private float _effectTimeInterval;
    [SerializeField] private float _currentInterval;
    [SerializeField] private float _effectDamage;

    private void Update()
    {
        _despawnTime -= Time.deltaTime;

        if (_despawnTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_currentInterval <= 0)
        {
            IDamageable damageable;
            if (collision.GetComponent<IEnemy>() != null && collision.TryGetComponent<IDamageable>(out damageable))
            {
                damageable.DoDamage(_effectDamage);
            }
            _currentInterval = _effectTimeInterval;
        }
        _currentInterval -= Time.deltaTime;
    }
}

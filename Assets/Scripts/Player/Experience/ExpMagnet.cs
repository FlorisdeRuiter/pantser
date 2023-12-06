using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ExpMagnet : MonoBehaviour
{
    [SerializeField] private float _magnetRadius;

    private CircleCollider2D _magnetTrigger;

    private void Start()
    {
        _magnetTrigger = GetComponent<CircleCollider2D>();
        _magnetRadius = _magnetTrigger.radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ExpPoint inRangeExp) && !inRangeExp.IsMoving)
        {
            inRangeExp.StartSmoothMoveToObject(gameObject);
        }
    }

    public void UpdateTriggerRadius(float radius)
    {
        _magnetTrigger.radius = _magnetRadius;
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExpPoint : PoolItem, IPickupable
{
    public int ExpValue;
    [SerializeField] private float _timeTillPlayerReached;

    private ExperienceManager experienceManager;

    public bool IsMoving = false;

    private void Start()
    {
        experienceManager = GameManager.GetInstance().ExperienceManager;
    }

    public void PickUp()
    {
        experienceManager.OnGainExp(ExpValue);
        ReturnToPool();
    }

    public void StartSmoothMoveToObject(GameObject targetObject)
    {
        if (targetObject.activeInHierarchy)
        {
            StartCoroutine(SmoothMoveToObject(targetObject));
        }
    }

    private IEnumerator SmoothMoveToObject(GameObject targetObject)
    {
        float timeElapsed = 0;
        Vector3 startPosition = transform.position;
        while (timeElapsed <= _timeTillPlayerReached)
        {
            transform.position = Vector3.Lerp(startPosition, targetObject.transform.position, SmoothTime(timeElapsed / _timeTillPlayerReached));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetObject.transform.position;
    }

    private float SmoothTime(float t)
    {
        return t * t * (3f - 2f * t);
    }

    private void OnEnable()
    {
        IsMoving = false;
    }
}

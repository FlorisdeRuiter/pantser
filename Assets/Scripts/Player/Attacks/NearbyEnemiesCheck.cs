using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NearbyEnemiesCheck : MonoBehaviour
{
    private Player player;
    private static NearbyEnemiesCheck _instance;

    public List<Transform> nearbyEnemyList;

    private BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        player = Player.GetInstance();
        SetColliderToCameraSize();
    }

    #region Adding and Removing Enemies from List
    /// <summary>
    /// Adds enemy to list when it enters the collider
    /// </summary>
    /// <param name="collision">The enemy's collider</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy;
        if (collision.gameObject.TryGetComponent<IEnemy>(out enemy))
        {
            nearbyEnemyList.Add(enemy.GetTransform());
        }
    }

    /// <summary>
    /// Removes enemy from nearby enemies list when it exits the collider or deactivates.
    /// </summary>
    /// <param name="collision">The enemy's collider</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        IEnemy enemy;
        if (collision.gameObject.TryGetComponent<IEnemy>(out enemy))
        {
            nearbyEnemyList.Remove(enemy.GetTransform());
        }
    }
    #endregion

    #region Get Closest Enemy
    /// <summary>
    /// Loops through list of nearby enemies and gets their tranform.
    /// Compares the distance of each enemy from the player.
    /// Enemy with the least distance is bestTarget.
    /// returns bestTarget
    /// </summary>
    public Transform GetNearestEnemy()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = player.transform.position;
        foreach (Transform potentialTarget in nearbyEnemyList)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
    #endregion

    #region Get Random Enemy
    /// <summary>
    /// Selects random enemy from list of nearby enemies.
    /// </summary>
    public Transform GetRandomEnemy()
    {
        return nearbyEnemyList[Random.Range(0, nearbyEnemyList.Count)];
    }
    #endregion

    #region Sets Collider to Camera's size
    public void SetColliderToCameraSize()
    {
        Camera cam = Camera.main;

        if (!cam.orthographic)
        {
            Debug.LogError("Camera is not set to orthographic");
        }

        float aspect = (float)Screen.width / Screen.height;
        float orthoSize = cam.orthographicSize;

        float width = 2.0f * orthoSize * aspect;
        float height = 2.0f * orthoSize;

        collider.size = new Vector2(width, height);
    }
    #endregion

    #region Get Instance
    public static NearbyEnemiesCheck GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<NearbyEnemiesCheck>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<NearbyEnemiesCheck>();
        }

        return _instance;
    }
    #endregion
}

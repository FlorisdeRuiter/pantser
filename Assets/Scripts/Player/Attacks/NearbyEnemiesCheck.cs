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

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        player = Player.GetInstance();
        SetColliderToCameraSize();
    }

    #region Adding and Removing Enemies from List
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Adds transform to list if collider entered the nearby enemy collider and has the IEnemy component
        IEnemy enemy;
        if (collision.gameObject.TryGetComponent<IEnemy>(out enemy))
        {
            nearbyEnemyList.Add(enemy.GetTransform());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Removes transform to list if collider exited the nearby enemy collider and has the IEnemy component 
        IEnemy enemy;
        if (collision.gameObject.TryGetComponent<IEnemy>(out enemy))
        {
            nearbyEnemyList.Remove(enemy.GetTransform());
        }
    }
    #endregion

    #region Get Closest Enemy
    public Transform GetNearestEnemy()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = player.transform.position;

        // Takes the transform from nearby enemies
        foreach (Transform potentialTarget in nearbyEnemyList)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            // Compares distance to player
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

        // Checks the camera's aspect ratio
        float aspect = (float)Screen.width / Screen.height;
        float orthoSize = cam.orthographicSize;

        // Calculates the correct width and height
        float width = 2.0f * orthoSize * aspect;
        float height = 2.0f * orthoSize;

        // Sets the colliders size equal to what the camera is displaying
        _collider.size = new Vector2(width, height);
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

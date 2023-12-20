using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner _instance;

    [Header("Enemy Spawn Offset")]
    [SerializeField] private Vector2 _spawnArea;

    public void SpawnEnemy(GameObject enemyToSpawn)
    {
        Vector3 position = GetRandomPosition();

        GameObject newEnemy = Instantiate(enemyToSpawn, transform);
        newEnemy.transform.position = GetRandomPosition();
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));

        float f = Random.value > 0.5f ? -1f : 1f;
        if (Random.value > 0.5f)
        {
            position.x += Random.Range(-_spawnArea.x, _spawnArea.x);
            position.y += _spawnArea.y * f;
        }
        else
        {
            position.y += Random.Range(-_spawnArea.y, _spawnArea.y);
            position.x += _spawnArea.x * f;
        }

        position.z = 0;

        return position;
    }

    public static EnemySpawner GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<EnemySpawner>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<EnemySpawner>();
        }

        return _instance;
    }
}

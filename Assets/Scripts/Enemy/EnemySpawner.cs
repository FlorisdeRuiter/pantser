using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner _instance;
    private Player player;

    [Header("Enemy Types")]
    public List<ObjectPool> EnemyPoolsList;                             //List of all enemyPools in game
    [SerializeField] private List<ObjectPool> _currentEnemiesInPoolsList;//List of current enemy types eligible for spawning
    [Tooltip("Seconds between adding new enemy pool to spawner")]
    [SerializeField, Range(0, 300)] private float _addEnemyPoolInterval; //Interval between adding enemy type to list

    [Header("Enemy Spawn Intervals")]
    [SerializeField, Range(0, 5)] private float _spawnInterval;          //Interval between enemies spawning

    [Header("Spawn Rate")]
    [Tooltip("Interval between spawn rate increases")]
    [SerializeField, Range(0, 10)] private float _spawnRateInterval;     //Interval between spawn rate increases
    [Tooltip("Amount spawnRateInterval deacreases by")]
    [SerializeField, Range(0f, 1f)] private float _spawnRateIncrease;    //Amount spawnRateInterval deacreases by

    [Header("Enemy Spawn Offset")]
    [SerializeField, Range(0f, 1f)] private float _offsetX, _offsetY;

    private void Start()
    {
        player = Player.GetInstance();
        StartCoroutine(SpawnEnemy(GetSpawnPosition()));
        StartCoroutine(IncreaseSpawnRate(_spawnRateInterval, _spawnRateIncrease));
        StartCoroutine(AddEnemyTypeCor());
    }

    #region Spawn Enemy
    /// <summary>
    /// Selects a random pool.
    /// Spawn a enemy from selected pool.
    /// Places enemy on spawnPos.
    /// </summary>
    /// <param name="spawnPos">Position the enemy will spawn in</param>
    private IEnumerator SpawnEnemy(Vector2 spawnPos)
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnInterval);

        yield return wait;

        // Selects a random enemy type to spawn
        ObjectPool currentPool = _currentEnemiesInPoolsList[Random.Range(0, _currentEnemiesInPoolsList.Count)];

        // Spawns enemy and sets its position
        GameObject e = currentPool.GetPooledObject(transform.position, transform.rotation, currentPool.gameObject.transform) as GameObject;
        e.transform.position = spawnPos;
        e.transform.rotation = Quaternion.identity;

        // Restarts the spawn cycle
        StartCoroutine(SpawnEnemy(GetSpawnPosition()));
    }

    /// <summary>
    /// Returns a random position for an enemy to spawn.
    /// </summary>
    private Vector2 GetSpawnPosition()
    {
        Camera cam = Camera.main;
        float posY = Random.Range(0f, 1f);
        float posX = Random.Range(0f, 1f);

        #region Offset
        //Checks what offset is needed.
        //Adds offset to the spawn position
        if (Mathf.Abs(posX - 0.5f) > Mathf.Abs(posY - 0.5f))
        {
            posX = Mathf.Round(posX);

            if (posX == 0)
                posX -= _offsetX;
            else
                posX += _offsetX;
        }
        else
        {
            posY = Mathf.Round(posY);
            if (posY == 0)
                posY -= _offsetY;
            else
                posY += _offsetY;
        }
        #endregion

        Vector2 spawnPos = cam.ViewportToWorldPoint(new Vector3(posX, posY));

        return spawnPos;
    }
    #endregion

    #region Add Enemy Types to Pool
    /// <summary>
    /// Adds a new enemy type to the pool after an interval.
    /// </summary>
    private IEnumerator AddEnemyTypeCor()
    {
        WaitForSeconds wait = new WaitForSeconds(_addEnemyPoolInterval);

        yield return wait;

        int newEnemyListNumber = _currentEnemiesInPoolsList.Count;

        if (newEnemyListNumber < EnemyPoolsList.Count)
        {
            ObjectPool newEnemyList = EnemyPoolsList[newEnemyListNumber];
            newEnemyList.gameObject.SetActive(true);
            AddEnemyTypeToSpawner(newEnemyList);
            StartCoroutine(AddEnemyTypeCor());
        }
    }

    private void AddEnemyTypeToSpawner(ObjectPool enemyType)
    {
        _currentEnemiesInPoolsList.Add(enemyType);
    }
    #endregion

    #region Spawn Rate Increase
    /// <summary>
    /// Increases the enemies spawn rate
    /// </summary>
    /// <param name="interval">After how many seconds spawn rate is increased</param>
    /// <param name="decrease">By how much the spawn rate is increased</param>
    /// <returns></returns>
    private IEnumerator IncreaseSpawnRate(float interval, float decrease)
    {
        WaitForSeconds wait = new WaitForSeconds(interval);

        yield return wait;

        _spawnInterval -= decrease / 1000;

        StartCoroutine(IncreaseSpawnRate(_spawnRateInterval, _spawnRateIncrease));
    }
    #endregion

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

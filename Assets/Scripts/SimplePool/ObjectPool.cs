using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// Item type that's in this ObjectPool
    /// Item type decided in inspector
    /// </summary>
    public GameObject PooledObject;
    /// <summary>
    /// Amount of items that can be in ObjectPool
    /// </summary>
    public int PoolSize;

    [Tooltip("Set this to true if you want to expand the pool if you run out of pooled objects.")]
    [SerializeField] private bool _autoExpand;

    [Tooltip("The amount of new objects added when the pool runs out of objects.")]
    [SerializeField] private int _expansionSize;

    /// <summary>
    /// The Stack that holds the ObjectPool's items
    /// </summary>
    private Stack<PoolItem> _objectPool;

    /// <summary>
    /// Defines ObjectPool and gives it a size
    /// </summary>
    private void OnEnable()
    {
        _objectPool = new Stack<PoolItem>(PoolSize);

        Expand(PoolSize);
    }

    #region Instantiate pooledObjects
    /// <summary>
    /// Instantiates amount of pooledObject based on expansionSize from ObjectPool
    /// Tells item this ObjectPool is it's ObjectPool
    /// </summary>
    /// <param name="expansionSize">Amount of items that need to be instantiated ()</param>
    private void Expand(int expansionSize)
    {
        for (int i = 0; i < expansionSize; i++)
        {
            GameObject newObject = Instantiate(PooledObject);
            PoolItem item = newObject.GetComponent<PoolItem>();
            item.Pool = this;
            ReturnPooledObject(item);
        }
    }
    #endregion

    #region Take pooledObject from ObjectPool
    /// <summary>
    /// Pops pooledObject from ObjectPool
    /// Activates pooledObject in scene
    /// Returns pooledObject
    /// </summary>
    /// /// <param name="position">The position where the item will be spawned</param>
    /// <param name="rotation">The rotation of the irem at the spawn position</param>
    /// <param name="parent">The requested parent for the item after it is spawned</param>
    public GameObject GetPooledObject(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        // Adds expansionSize to ObjectPool when it's empty
        if (_objectPool.Count == 0 && _autoExpand)
        {
            Expand(_expansionSize);
        }

        PoolItem item = _objectPool.Pop();
        item.Init(position, rotation, parent != null ? parent : transform);
        item.gameObject.SetActive(true);
        return item.gameObject;
    }
    #endregion

    #region Return pooledObject to ObjectPool
    /// <summary>
    /// Sets ObjectPool as pooledObject's parent
    /// Deactivates pooledObject in scene
    /// Pushes pooledObject in ObjectPool
    /// </summary>
    /// <param name="item"></param>
    public void ReturnPooledObject(PoolItem item)
    {
        // If pooledObject is not active return
        if (!item.gameObject.activeSelf)
        {
            return;
        }

        item.transform.parent = transform;
        item.gameObject.SetActive(false);

        _objectPool.Push(item);
    }
    #endregion
}


using UnityEngine;                                                                      

public abstract class PoolItem : MonoBehaviour
{
    // The object pool item belongs to.
    protected ObjectPool _myPool;

    /// <summary>
    /// Set the ObjectPool  belong to.
    /// </summary>
    public ObjectPool Pool { set { _myPool = value; } }

    #region Activate and deactivating PoolItem
    /// <summary>
    /// This is called just before this item is activated
    /// </summary>
    protected virtual void Activate() { }

    /// <summary>
    /// This is called just before the item is deactivated
    /// </summary>
    protected virtual void Deactivate() { }
    #endregion

    #region Initialization and returning to pool
    /// <summary>
    /// Initialized this item and activates it.
    /// </summary>
    /// <param name="position">The position where the item will be spawned</param>
    /// <param name="rotation">The rotation of the irem at the spawn position</param>
    /// <param name="parent">The requested parent for the item after it is spawned</param>
    public void Init(Vector3 position, Quaternion rotation, Transform parent)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.parent = parent;

        Activate();
    }

    /// <summary>
    /// Call this to return item to ObjectPool
    /// </summary>
    public void ReturnToPool()
    {
        Deactivate();

        _myPool.ReturnPooledObject(this);
    }
    #endregion
}

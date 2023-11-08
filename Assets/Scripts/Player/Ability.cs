using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Ability Base Class")]
    [SerializeField] private List<Transform> childrenList;

    private void Start()
    {
        //Puts all children under parent in a list
        foreach (Transform child in transform)
        {
            childrenList.Add(child);
        }
    }

    #region Enable and Disable Children
    /// <summary>
    /// Enables all children under parent
    /// </summary>
    public virtual void EnableChildren()
    {
        foreach (Transform child in childrenList)
        {
            child.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Disables all children under parent
    /// </summary>
    public virtual void DisableChildren()
    {
        foreach (Transform child in childrenList)
        {
            child.gameObject.SetActive(false);
        }
    }
    #endregion
}

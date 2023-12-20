using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingAnt : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;

    public void Explode()
    {
        GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
    }
}

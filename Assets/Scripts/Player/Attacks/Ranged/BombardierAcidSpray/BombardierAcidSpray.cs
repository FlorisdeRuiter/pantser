using UnityEngine;

public class BombardierAcidSpray : MonoBehaviour
{
    [SerializeField] private GameObject _areaEffect;

    public void PlaceAcidSplash()
    {
        Instantiate(_areaEffect, transform.position, Quaternion.identity);
    }
}

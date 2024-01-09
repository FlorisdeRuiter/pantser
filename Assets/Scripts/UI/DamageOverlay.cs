using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    [SerializeField] private Image _overlay;
    private PlayerHealth _playerHealth;
    [SerializeField] float _modifier;

    private void Start()
    {
        _playerHealth = GameManager.GetInstance().Player.GetComponent<PlayerHealth>();
    }

    public void Updateoverlay()
    {
        var tempColor = _overlay.color;
        tempColor.a = 1f - (( _playerHealth.CurrentHealth/_playerHealth.MaxHealth ) * _modifier);
        _overlay.color = tempColor;
    }
}

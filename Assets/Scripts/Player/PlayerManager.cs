using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerHealth PlayerHealth;

    private void Start()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
    }
}

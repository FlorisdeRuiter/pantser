using UnityEngine;

public class PestControl : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotationSpeed) * Time.deltaTime);
    }
}

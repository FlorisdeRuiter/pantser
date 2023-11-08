using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        Vector2 move = transform.right * xMove + transform.up * yMove;

        _characterController.Move(-move * _moveSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;

    [SerializeField] private float _horInput;
    [SerializeField] private float _verInput;

    private void Update()
    {
        // Uses input value to decide the player's walk direction
        transform.position = new Vector2
        (transform.position.x + (_walkSpeed * _horInput * Time.deltaTime),
        transform.position.y + (_walkSpeed * _verInput * Time.deltaTime));
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Reads input value received by unity's input system
        _horInput = context.ReadValue<Vector2>().x;
        _verInput = context.ReadValue<Vector2>().y;
    }
}
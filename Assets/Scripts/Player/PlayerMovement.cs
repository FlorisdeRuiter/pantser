using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;

    [SerializeField] public float HorInput;
    [SerializeField] public float VerInput;

    private void Update()
    {
        // Uses input value to decide the player's walk direction
        transform.position = new Vector2
        (transform.position.x + (_walkSpeed * HorInput * Time.deltaTime),
        transform.position.y + (_walkSpeed * VerInput * Time.deltaTime));
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Reads input value received by unity's input system
        HorInput = context.ReadValue<Vector2>().x;
        VerInput = context.ReadValue<Vector2>().y;
    }
}
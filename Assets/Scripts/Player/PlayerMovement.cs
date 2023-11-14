using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;

    [SerializeField] private float horInput;
    [SerializeField] private float verInput;

    private void Update()
    {
        transform.position = new Vector2
        (transform.position.x + (walkSpeed * horInput * Time.deltaTime),
        transform.position.y + (walkSpeed * verInput * Time.deltaTime));
    }

    public void Move(InputAction.CallbackContext context)
    {
        horInput = context.ReadValue<Vector2>().x;
        verInput = context.ReadValue<Vector2>().y;
    }
}
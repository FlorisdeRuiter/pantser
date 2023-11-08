using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;

    [SerializeField] private float horInput;
    [SerializeField] private float verInput;
    [SerializeField] Vector2 target;

    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.GetInstance();
    }

    private void Start()
    {
        target = transform.position;
    }

    private void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        verInput = Input.GetAxis("Vertical");

        Walk();
    }

    #region Moves Player
    /// <summary>
    /// Checks the player's chosen input type.
    /// Moves the player.
    /// </summary>
    private void Walk()
    {
        //Controls for keyboard and controller.
        //Adds to the players position based on it's speed and current input on the axis.
        if (inputManager.inputType == InputManager.MovementInputType.Keyboard || inputManager.inputType == InputManager.MovementInputType.Controller)
        {
            if (horInput != 0 || verInput != 0)
            {
                transform.position = new Vector2
                (transform.position.x + (walkSpeed * horInput * Time.deltaTime),
                transform.position.y + (walkSpeed * verInput * Time.deltaTime));
            }
        }

        //Controls for mouse.
        //Checks position on the screen where mouse click happens.
        //translates it to a position in the world.
        //Moves the player to that position.
        if (inputManager.inputType == InputManager.MovementInputType.Mouse)
        {
            if (Input.GetMouseButton(0))
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            transform.position = Vector2.MoveTowards(transform.position, target, walkSpeed * Time.deltaTime);
        }
    }
    #endregion
}

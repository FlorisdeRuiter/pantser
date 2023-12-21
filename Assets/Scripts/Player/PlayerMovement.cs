using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;

    public float HorInput;
    public float VerInput;

    public Vector2 movementInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Uses input value to decide the player's walk direction
        //transform.position = new Vector2
        //(transform.position.x + (_walkSpeed * HorInput * Time.deltaTime),
        //transform.position.y + (_walkSpeed * VerInput * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _walkSpeed * movementInput;
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Reads input value received by unity's input system
        HorInput = context.ReadValue<Vector2>().x;
        VerInput = context.ReadValue<Vector2>().y;

        movementInput = context.ReadValue<Vector2>();
    }
}
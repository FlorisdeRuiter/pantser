using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private Animator _animator;
    private Vector2 _lastMovementInput;

    public Vector2 MovementInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Changes animator values if the movement values changed since the last frame
        if (_animator != null)
        {
            if (MovementInput.x != _lastMovementInput.x && MovementInput.x != 0)
            {
                _animator.SetFloat("xMove", MovementInput.x > 0 ? 1 : -1);
                _animator.SetBool("isWalking", true);
            }
            else if (MovementInput.x == 0)
            {
                _animator.SetBool("isWalking", false);
            }
        }

        _lastMovementInput = MovementInput;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _walkSpeed * MovementInput;;
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Reads input value received by unity's input system
        MovementInput = context.ReadValue<Vector2>();
    }
}
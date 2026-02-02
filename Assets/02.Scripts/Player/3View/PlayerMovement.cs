using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Animator _animator;
    Camera _camera;
    CharacterController _controller;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 8f;
    private float finalSpeed = 0;
    private bool run;

    private bool jumpPressed;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float gravity = -20;
    private Vector3 velocity;
    private bool wasGrounded;


    private Vector3 moveInput;

    [SerializeField] private bool toggleCameraRotation;

    [SerializeField] private float smoothness = 10f;

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
        _camera = Camera.main;
        _controller = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        Debug.Log("∂•¿Œ∞°: "+ _controller.isGrounded);
        if (Keyboard.current.leftAltKey.isPressed)
            toggleCameraRotation = true;
        else
            toggleCameraRotation = false;

        InputMovement();
    }

    private void InputMovement()
    {
        finalSpeed = (run) ? runSpeed : speed;

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        if (_controller.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f;

            if (jumpPressed)
            {
                velocity.y = jumpForce;
                jumpPressed = false;
            }
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = moveDirection.normalized * finalSpeed;
        finalMove.y = velocity.y;

        _controller.Move(finalMove * Time.deltaTime);

        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
        _animator.SetFloat("WorkBlend", percent, 0.1f, Time.deltaTime);


        bool isGrounded = _controller.isGrounded;

        _animator.SetBool("IsGround", isGrounded);
        wasGrounded = isGrounded;


        UpdateJumpAnimation();
    }

    private void UpdateJumpAnimation()
    {
        if (!_controller.isGrounded)
        {
            if (velocity.y > 0.1f)
                _animator.SetFloat("JumpBlend", 0f);
            else
                _animator.SetFloat("JumpBlend", 0.5f);
        }
        else
        {
            if (Mathf.Abs(velocity.y) < 0.1f)
                _animator.SetFloat("JumpBlend", 1f);
        }
    }

    private void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        run = context.ReadValueAsButton();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
    }
}

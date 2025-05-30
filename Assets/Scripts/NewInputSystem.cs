using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputSystem : MonoBehaviour
{
    public float speedWalk, speedSprint, jumpPower;

    private bool isSprinting, isJumping;

    private Vector2 moveDirection;

    private void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Move"].performed += MoveEvent;
        playerInput.actions["Move"].canceled += MoveEvent;

        playerInput.actions["Sprint"].performed += SprintEvent;
        playerInput.actions["Sprint"].canceled += SprintEvent;
    }

    void Update()
    {
        transform.position += new Vector3(moveDirection.x, isJumping ? jumpPower : 0f, moveDirection.y) * Time.deltaTime * (isSprinting == true ? speedSprint : speedWalk);
    }

    public void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
    }

    public void MoveEvent(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputValue input)
    {
        isSprinting = input.isPressed;
    }

    public void SprintEvent(InputAction.CallbackContext context)
    {
        isSprinting = context.performed;
    }

    public void JumpEvent(InputAction.CallbackContext context)
    {
        isJumping = context.performed;
    }
}

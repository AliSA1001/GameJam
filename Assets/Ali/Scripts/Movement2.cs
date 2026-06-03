using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Movement2 : MonoBehaviour
{
    private CharacterController characterController;

    [Header("moveSetings")]
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float rotationSpeed;


    private float vertcalMovement;
    private float horizcalMovement;
    private Vector3 velocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Vector3 direction = new Vector3(horizcalMovement, 0f, vertcalMovement).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calculate where we need to look
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards that target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        characterController.Move(direction * Time.deltaTime * speed);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        vertcalMovement = context.ReadValue<Vector2>().y;
        horizcalMovement = context.ReadValue<Vector2>().x;

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}

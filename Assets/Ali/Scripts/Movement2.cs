using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Movement2 : MonoBehaviour
{
    private CharacterController characterController;

    [Header("moveSetings")]
    [SerializeField] private bool canMove = true;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float rotationSpeed;


    private float vertcalMovement;
    private float horizcalMovement;
    private Vector3 velocity;

    // Animation 
    private Animator animator;
    private bool isWalking;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if(vertcalMovement == 0 && horizcalMovement == 0)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Vector3 direction = new Vector3(horizcalMovement, 0f, vertcalMovement).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (canMove)
        {
            characterController.Move(direction * Time.deltaTime * speed);

            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);
        }
        
    }
    public void SwitchMoveState(bool newstate)
    {
       canMove = newstate;
       canMove = newstate;
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

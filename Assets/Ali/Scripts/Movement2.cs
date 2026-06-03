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


    private float vertcalMovement;
    private float horizcalMovement;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(horizcalMovement, gravity, vertcalMovement);
        characterController.Move(move * Time.deltaTime * speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        vertcalMovement = context.ReadValue<Vector2>().y;
        horizcalMovement = context.ReadValue<Vector2>().x;

    }
}

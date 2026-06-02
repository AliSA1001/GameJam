using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float gravity = -9.7f;

    private Rigidbody rb;
    private float moveX;
    private float moveY;



    private void Awake()
    {
         rb = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        Vector3 move = new Vector3 (moveX * speed, gravity ,moveY * speed);
        rb.linearVelocity = move;
    }



    public void MoveInput(InputAction.CallbackContext context)
    {
        moveX = context.ReadValue<Vector2>().x;
        moveY = context.ReadValue<Vector2>().y;

    }
}

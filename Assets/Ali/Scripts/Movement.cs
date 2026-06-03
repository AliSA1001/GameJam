using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Move Swttings")]
    [SerializeField] private float speed = 5;
    private Vector2 moveInput;

    [Header("Jump Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private float GroundDistanceCheck;
    [SerializeField] private bool OntheGround;
    [SerializeField] private Transform groundcheckPoint;
    [SerializeField] private bool canDoubleJump = false; // will be true ONLY WHEN WE JUMP FOR THE FIRST TIME!!!
    private bool wasGorunded;


   // [Header("FeedBack")]
   // [SerializeField] private MMF_Player JumpFeedBack;
   // [SerializeField] private MMF_Player LandingFeedback;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void HandleOnLanding()
    {
      //  LandingFeedback.PlayFeedbacks();

    }
    private void Update()
    {

        // here we check for ground every frame ( becuse we are in update it will run every frame)
        HandleGroundCheck();

    }
    private void FixedUpdate()
    {
        float horizantal = moveInput.x;
        float vertical = moveInput.y;
        Vector3 move = new Vector3(horizantal * speed, rb.linearVelocity.y, vertical * speed);
        rb.linearVelocity = move;

    }

    private void HandleGroundCheck()
    {
        // we save the condation of the ground from last frame
        wasGorunded = OntheGround;

        // we just draw the raycast so we can see it
        Debug.DrawRay(groundcheckPoint.transform.position, Vector3.down * GroundDistanceCheck, Color.red);
        // here we just send a line that starts from  groundcheckPoint with Vector.down as the way it will go with 
        // GroundDistanceCheck is the length of our line 
        // groundLayer is layer that we call ground in our world 
        OntheGround = (Physics.Raycast(groundcheckPoint.transform.position, Vector3.down, GroundDistanceCheck, groundLayer));

        // this means that last frame we were above ground and now we are on the ground on this frame so we just landed
        if (!wasGorunded && OntheGround)
        {
           // HandleOnLanding();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // here we just check if we clicked Space AND if we are on the ground (if any of them is false we dont jump ) and doubleJump
        if (context.started && (OntheGround || canDoubleJump))
        {
            // this line will help a lot when we do the double jump later
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

            // we just add the force with type Impulse (Impulse Is doing the full force of our jump in one frame )
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // if feed back is not null play it
           // JumpFeedBack?.PlayFeedbacks();


            if (canDoubleJump)
            {
                canDoubleJump = false;
            }
            else
            {
                canDoubleJump = true;
            }
        }
    }
}

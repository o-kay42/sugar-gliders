using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Movement speed of the player")]
    public float moveSpeed = 5.0f;
    [Tooltip("Jump force applied to the player")]
    public float jumpForce = 7.0f;
    [Tooltip("Sprint speed of the player")]
    public float sprintSpeed = 10.0f;
    [Tooltip("Climb force applied to the player")]
    public float climbForce = 7.0f;
    [Tooltip("Gravity applied to the player while gliding")]
    public float glideGravity = 0.5f;

    private Rigidbody2D playerRB;
    public GameObject climbObjects;
    private float _movement;
    private bool isJumping = false;
    private bool isClimb = false;
    public ClimbControl climbControlReference;
    private BoxCollider2D boxColl;
    [SerializeField] LayerMask jumpableGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.linearVelocityX = _movement;
        isClimb = climbControlReference.isClimbing;
        if (isGrounded())
        {
            playerRB.gravityScale = 3f;
        }
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * moveSpeed;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 1 && isGrounded())
        {
            playerRB.linearVelocityY = jumpForce;
            // search box cast for tut on this
        }
    }
    public void Sprint(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * sprintSpeed;
    }

    //climbing
    public void Climb(InputAction.CallbackContext ctx)
    {
        if (isClimb == true)
        {
            _movement = ctx.ReadValue<Vector2>().y * climbForce;
            //playerRB.linearVelocityY = climbForce;
        }
    }

    public void Glide(InputAction.CallbackContext ctx)
    {
        playerRB.gravityScale = glideGravity;
        //once on ground, change back
    }

}

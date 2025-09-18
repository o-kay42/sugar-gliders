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
    [Tooltip("Glide speed of the player")]
    public float glideSpeed = 6.0f;
    [Tooltip("Speed of the player at any moment")]
    private float currentMoveSpeed = 0.0f;

    private Rigidbody2D playerRB;
    private float _movement;
    private bool isClimbable = false;
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
        isClimbable = climbControlReference.isClimbing;
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * currentMoveSpeed;
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 1 && isGrounded())
        {
            playerRB.linearVelocityY = jumpForce;
        }
    }
    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 0)
        {
            currentMoveSpeed = moveSpeed;
            return;
        }
        currentMoveSpeed = sprintSpeed;
    }
    public void Climb(InputAction.CallbackContext ctx)
    {
        if (isClimbable)
        {
            playerRB.linearVelocityY = climbForce * ctx.ReadValue<Vector2>().y;
        }
    }
    public void Glide(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 0)
        {
            playerRB.gravityScale = 3f;
            currentMoveSpeed = moveSpeed;
            return;
        }
        playerRB.gravityScale = glideGravity;
        currentMoveSpeed = glideSpeed;

    }

}

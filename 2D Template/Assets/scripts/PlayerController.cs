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

    public Animator animator;

    private Rigidbody2D playerRB;
    private float _movement;
    private bool isClimbable = false;
    public ClimbControl climbControlReference;
    private BoxCollider2D boxColl;
    [SerializeField] LayerMask jumpableGround;
    [SerializeField] private SpriteRenderer playerSR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
        playerSR = GetComponent<SpriteRenderer>();

        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.linearVelocityX = _movement;
        isClimbable = climbControlReference.isClimbing;
        animator.SetFloat("speed", Mathf.Abs(_movement));
        if (isGrounded() == true)
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isGliding", false);
            animator.SetBool("canSlide", false);
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0)
        {
            playerSR.flipX = true;
        }
        else if (horizontalInput < 0)
        {
            playerSR.flipX = false;
        }
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
        animator.SetBool("isJump", true);
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
        animator.SetBool("climbing", true);
        if (isClimbable)
        {
            playerRB.gravityScale = 0f;
            playerRB.linearVelocityY = climbForce * ctx.ReadValue<Vector2>().y * 0.75f;
        }
        if (ctx.ReadValue<float>() == 0)
        {
            playerRB.gravityScale = 3f;
            animator.SetBool("climbing", false);
            animator.SetBool("canSlide", true);
            return;
        }
    }
    public void Glide(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 0)
        {
            playerRB.gravityScale = 3f;
            currentMoveSpeed = moveSpeed;
            animator.SetBool("isGliding", false);
            return;
        }
        animator.SetBool("isGliding", true);
        playerRB.gravityScale = glideGravity;
        currentMoveSpeed = glideSpeed;

    }

}

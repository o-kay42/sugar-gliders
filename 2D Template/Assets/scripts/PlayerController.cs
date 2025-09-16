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
    private Rigidbody2D playerRB;
    private float _movement;
    private bool isJumping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.linearVelocityX = _movement;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * moveSpeed;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 1 && isJumping == false)
        {
            playerRB.linearVelocityY = jumpForce;
        }
    }
    public void Sprint(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * sprintSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].point.y < transform.position.y)
        {
            isJumping = false;
        }
    }
    // search box cast for tut on this
}

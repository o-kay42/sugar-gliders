using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbControl : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float climbForce = 7.0f;
    public bool isClimbing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isClimbing = true;
        Debug.Log("in climb object");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isClimbing = false;
        Debug.Log("out of climb object");
    }
}

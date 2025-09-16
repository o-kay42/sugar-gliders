using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Climb : MonoBehaviour
{
    Rigidbody2D PlayerRigidbody2D;
    public float climbForce = 7.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space) && (collision.gameObject.tag == "Player"))
        { 
            Debug.Log("trigger active");
            PlayerRigidbody2D.AddForce(new Vector2(0, climbForce));
        }
    }
}
